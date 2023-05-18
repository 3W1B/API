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
    private MyDbContext _context = new();

    public async Task<CustomResponse> Create()
    {
        CustomResponse? customResponse = null;
        LogInside? logInside = null;
        LogOutside? logOutside = null;
        var log = await Validator.Body<Log>(Request.Body, d => LogRequest.Create(d, out customResponse, out logInside, out logOutside));
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
   
    /**
     * Returns all logs in a 24 hour period
     */
    public async Task<CustomResponse> ReadAll()
    {
        CustomResponse? customResponse = null;
        var log = await Validator.Body<Log>(Request.Body, d => LogRequest.ReadAll(d, out customResponse));
        if (customResponse is not null)
            return customResponse;

        var logs = from l in _context.Logs
            where l.RadonLoggerId == log!.RadonLoggerId && l.Timestamp > DateTime.Now.AddDays(-1)
            select l;
        
        if (!await logs.AnyAsync())
            return new CustomResponse("error", "No logs found");
        
        foreach (var l in logs)
        {
            _context = new MyDbContext();
            LogInside? logInside = await _context.LogInsides.FirstOrDefaultAsync(l => l.LogId == l.Id);
            LogOutside? logOutside = await _context.LogOutsides.FirstOrDefaultAsync(l => l.LogId == l.Id);
            l.LogInsides.Add(logInside);
            l.LogOutsides.Add(logOutside);
        }

        return new CustomResponse("success", "Logs found", await logs.ToListAsync());
    }
}