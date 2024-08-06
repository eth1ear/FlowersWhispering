using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowersWhisperingAPI.Data;
using FlowersWhisperingAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FlowersWhisperingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FlowersWhisperingContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(FlowersWhisperingContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
public async Task<IActionResult> Register(UserRegisterDto userDto)
{
    // 检查邮箱是否已存在
    if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
    {
        return BadRequest("Email already exists.");
    }

    // 检查用户名是否已存在
    if (await _context.Users.AnyAsync(u => u.UserName == userDto.UserName))
    {
        return BadRequest("Username already exists.");
    }

    // 创建新用户
    var user = new User
    {
        UserName = userDto.UserName,
        Email = userDto.Email,
        Password = userDto.Password, // 密码未加密直接存储
        UserStatus = "Active", // 直接设置状态
        UserRole = "User",
        LanguagePreference = "cn-ZH",
        RegistrationDate = DateTime.UtcNow.AddHours(8) // 处理时区
    };

    // 添加到数据库
    _context.Users.Add(user);
    try
    {
        await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        // 如果出现数据库错误，返回错误信息
        return BadRequest($"An error occurred while registering the user: {ex.Message}");
    }

    // 成功创建用户后返回
    return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
}



        [HttpPost("login")]
public async Task<ActionResult<dynamic>> Login(UserLoginDto loginDto)
{
    var user = await _context.Users
                     .FirstOrDefaultAsync(u => u.Email == loginDto.Login || u.UserName == loginDto.Login);

    if (user == null || user.Password != loginDto.Password) // 直接比较明文密码
        return Unauthorized("Invalid login credentials.");

    var token = GenerateJwtToken(user);

    return Ok(new 
    {
        UserId = user.UserId,
        UserName = user.UserName,
        UserRole = user.UserRole,
        Token = token
    });
}


        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? string.Empty));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return user;
        }
    }
}
