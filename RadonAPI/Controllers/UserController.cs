using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadonAPI.Context;
using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Requests;

namespace RadonAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    private readonly MyDbContext _context = new();
    
    [HttpPost]
    [Route(nameof(Register))]
    public async Task<CustomResponse> Register()
    {
        CustomResponse? customResponse = null;
        var user = await BodyHandler.Convert<User>(Request.Body, d => UserRequest.Register(d, out customResponse));
        if (customResponse is not null)
            return customResponse;

        var users = from u in _context.Users
            where u.Email == user!.Email || u.Phone == user.Phone
            select u;
        
        if (await users.AnyAsync())
            return new CustomResponse("error", "User with this email or phone already exists");

        await _context.Users.AddAsync(user!);
        await _context.SaveChangesAsync();
        
        return new CustomResponse("success", "User created", user);
    }
    
    [HttpPost]
    [Route(nameof(Login))]
    public async Task<CustomResponse> Login()
    {
        CustomResponse? customResponse = null;
        var user = await BodyHandler.Convert<User>(Request.Body, d => UserRequest.Login(d, out customResponse));
        if (customResponse is not null)
            return customResponse;

        var users = from u in _context.Users
            where u.Email == user!.Email
            select u;
        
        if (!await users.AnyAsync())
            return new CustomResponse("error", "User with this email does not exist");

        var userDb = await users.FirstOrDefaultAsync();
        userDb!.UserRadonLoggers = await _context.UserRadonLoggers.Where(u => u.UserId == userDb.Id).ToListAsync();
        
        return BCrypt.Net.BCrypt.EnhancedVerify(user!.Password, userDb.Password) 
            ? new CustomResponse("success", "User logged in", userDb)
            : new CustomResponse("error", "Password is incorrect");
    }
    
    [HttpPost]
    [Route(nameof(AddLogger))]
    public async Task<CustomResponse> AddLogger()
    {
        CustomResponse? customResponse = null;
        var d = await BodyHandler.Convert<dynamic>(Request.Body, d => UserRequest.AddLogger(d, out customResponse));
        if (customResponse is not null)
            return customResponse;
        
        int userId = int.Parse(d!.UserId.ToString());
        int radonLoggerId = int.Parse(d.RadonLoggerId.ToString());
        string radonLoggerPassword = d.RadonLoggerPassword.ToString();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
            return new CustomResponse("error", "User does not exist");
        
        var logger = await _context.RadonLoggers.FirstOrDefaultAsync(l => l.Id == radonLoggerId);
        if (logger is null)
            return new CustomResponse("error", "Logger does not exist");
        
        var userLogger = await _context.UserRadonLoggers.FirstOrDefaultAsync(ul => ul.UserId == user.Id && ul.RadonLoggerId == logger.Id);
        if (userLogger is not null)
            return new CustomResponse("error", "User already has this logger");
        
        if (!BCrypt.Net.BCrypt.EnhancedVerify(radonLoggerPassword, logger.Password))
            return new CustomResponse("error", "Password for logger is incorrect");

        var userRadonLogger = new UserRadonLogger
        {
            UserId = user.Id,
            RadonLoggerId = logger.Id
        };
        
        await _context.UserRadonLoggers.AddAsync(userRadonLogger);
        await _context.SaveChangesAsync();
        
        return new CustomResponse("success", "Logger added to user", userRadonLogger);
    }
    
}