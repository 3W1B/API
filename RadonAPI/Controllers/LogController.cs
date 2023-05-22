using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        LogInside? logInside = null;
        LogOutside? logOutside = null;
        var log = await BodyHandler.Convert<Log>(Request.Body, d => LogRequest.Create(d, out customResponse, out logInside, out logOutside));
        if (customResponse is not null)
            return customResponse;
        
        await _context.Logs.AddAsync(log!);
        await _context.SaveChangesAsync();
        
        logInside!.LogId = log!.Id;
        logOutside!.LogId = log.Id;
        
        await _context.LogInsides.AddAsync(logInside);
        await _context.LogOutsides.AddAsync(logOutside);
        await _context.SaveChangesAsync();
        
        return new CustomResponse("success", "Log created", log);
    }
}