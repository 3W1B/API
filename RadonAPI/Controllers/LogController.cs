using Microsoft.AspNetCore.Mvc;
using RadonAPI.Context;
using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Requests;

namespace RadonAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LogController : ControllerBase
{
    private readonly MyDbContext _context = new();

    [HttpPost]
    [Route(nameof(Create))]
    public async Task<CustomResponse> Create()
    {
        CustomResponse? customResponse = null;
        var d = await BodyHandler.Convert<dynamic>(Request.Body, d => LogRequest.Create(d, out customResponse));
        if (customResponse is not null)
            return customResponse;

        var dbLogger = await _context.Loggers.FindAsync(d!.Log.LoggerId);
        
        if (!BCrypt.Net.BCrypt.EnhancedVerify(d.LoggerPassword, dbLogger!.Password))
            return new CustomResponse("error", "Logger password is incorrect");
        
        await _context.Logs.AddAsync(d.Log);
        await _context.SaveChangesAsync();

        return new CustomResponse("success", "Log created", d);
    }
}