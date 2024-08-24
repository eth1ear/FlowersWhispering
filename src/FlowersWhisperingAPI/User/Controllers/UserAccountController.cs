using FlowersWhisperingAPI.User.Services.Interface;
using FlowersWhisperingAPI.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersWhisperingAPI.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController(IUserAccountService userAccountService) : ControllerBase
    {
        private readonly IUserAccountService _userAccountService = userAccountService;

        //用户登录
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            if(_userAccountService.IsHaveUsername(loginDTO.Username)){
                string password = _userAccountService.GetPasswordByUsername(loginDTO.Username);
                if(password == loginDTO.Password)
                    return Ok(new { Message = "登录成功",
                        userID = _userAccountService.GetUserIdByUsername(loginDTO.Username)});
                else
                    return Unauthorized("密码错误");
            }else if(_userAccountService.IsHaveEmail(loginDTO.Username)){
                string password = _userAccountService.GetPasswordByEmail(loginDTO.Username);//把传来的用户名作为邮箱
                if(password == loginDTO.Password)
                    return Ok(new { Message = "登录成功", 
                        userID = _userAccountService.GetUserIdByEmail(loginDTO.Username)});
                else
                    return Unauthorized("密码错误");
            }
            return BadRequest("未查到此账户，请先注册");
        }

        //用户注册
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO userDTO)
        {
            if (_userAccountService.IsHaveUsername(userDTO.Username))
                return BadRequest("注册失败，用户名已有人使用");
            if (_userAccountService.IsHaveEmail(userDTO.Email))
                return BadRequest("注册失败，邮箱已有人使用");
            string username = userDTO.Username;
            string password = userDTO.Password;
            string email = userDTO.Email;
            string languagePreference = userDTO.LanguagePreference;
            UserInfo userRegister = new UserInfo(username, password, email, languagePreference);
            bool result = _userAccountService.Register(userRegister);
            if (result)
                return Ok("注册成功");
            else
                return BadRequest("注册失败");
       }

       //用户资料编辑
       [HttpPut("edit/user")]
       public IActionResult UserEdit([FromBody] UserDTO userDTO)
       {
            if (_userAccountService.IsHaveUsername(userDTO.Username))
                return BadRequest("编辑失败，用户名已有人使用");
            if (_userAccountService.IsHaveEmail(userDTO.Email))
                return BadRequest("编辑失败，邮箱已有人使用");
            bool result = _userAccountService.UserUpdate(userDTO);
            if (result)
                return Ok("编辑成功");
            else
                return BadRequest("编辑失败");
       }
    }
}