using FlowersWhisperingAPI.Data;
using Oracle.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using FlowersWhisperingAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// 添加数据库上下文

builder.Services.AddDbContext<FlowersWhisperingContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDbConnection")));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 添加控制器支持
builder.Services.AddControllers();

// 添加CORS支持
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://example.com"));
});

// 添加JWT认证服务
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtKey = builder.Configuration["Jwt:Key"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey != null ? Encoding.UTF8.GetBytes(jwtKey) : throw new ArgumentNullException("Jwt:Key")),
    };
});


var app = builder.Build();
var scope = app.Services.CreateScope();
// try
// {
//     // 获取DbContext实例
//     var db = scope.ServiceProvider.GetRequiredService<FlowersWhisperingContext>();

//     // 尝试写入一条新的用户数据
//     var newUser = new User
//     {
//         UserName = "newUser",
//         Password = "password123",
//         Email = "newuser@example.com",
//         RegistrationDate = DateTime.Now,
//         UserRole = "User",
//         UserStatus = "Active",
//         LanguagePreference = "en-US"
//     };
//     db.Users.Add(newUser);
//     db.SaveChanges();
//     Console.WriteLine("New user added successfully.");

// }
// catch (OracleException ex)
// {
//     // 捕捉到Oracle数据库的异常
//     Console.WriteLine("Oracle error code: " + ex.Number);
//     Console.WriteLine("Details: " + ex.Message);
//     // 这里可以添加更多的日志记录或错误处理代码
// }
// catch (Exception ex)
// {
//     // 捕捉到其他类型的异常
//     Console.WriteLine("An error occurred: " + ex.Message);
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// 使用CORS
app.UseCors("AllowSpecificOrigin");

// 使用身份验证和授权
app.UseAuthentication();
app.UseAuthorization();

// 映射控制器
app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
