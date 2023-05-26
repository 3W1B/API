using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadonAPI.Context;
using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Requests;

namespace RadonAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserLoggerController : ControllerBase
{
    private readonly MyDbContext _context = new();
    
    [HttpPost]
    [Route(nameof(Create))]
    public async Task<CustomResponse> Create()
    {
        CustomResponse? customResponse = null;
        var d = await BodyHandler.Convert<dynamic>(Request.Body, d => UserLoggerRequest.Create(d, out customResponse));
        if (customResponse is not null)
            return customResponse;

        int userId = int.Parse(d!.UserId.ToString());
        string loggerId = d.LoggerId.ToString();
        string loggerPassword = d.LoggerPassword.ToString();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
            return new CustomResponse("error", "User does not exist");

        var logger = await _context.Loggers.FirstOrDefaultAsync(l => l.Id == loggerId);
        if (logger is null)
            return new CustomResponse("error", "Logger does not exist");

        if (await _context.UserLoggers.AnyAsync(ul => ul.UserId == user.Id && ul.LoggerId == logger.Id))
            return new CustomResponse("error", "User already has this logger");

        if (!BCrypt.Net.BCrypt.EnhancedVerify(loggerPassword, logger.Password))
            return new CustomResponse("error", "Password for logger is incorrect");

        var userLogger = new UserLogger
        {
            UserId = user.Id,
            LoggerId = logger.Id
        };

        await _context.UserLoggers.AddAsync(userLogger);
        await _context.SaveChangesAsync();

        return new CustomResponse("success", "Logger added to user", userLogger);
    }
}