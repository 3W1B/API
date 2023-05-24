using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadonAPI.Context;
using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Requests;

namespace RadonAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LoggerController : ControllerBase
{
    private MyDbContext _context = new();
    
    [HttpPost]
    [Route(nameof(Read))]
    public async Task<CustomResponse> Read()
    {
        CustomResponse? customResponse = null;
        var logger = await BodyHandler.Convert<Logger>(Request.Body, d => LoggerRequest.Read(d, out customResponse));
        if (customResponse is not null)
            return customResponse;

        var loggers = from rl in _context.Loggers
            where rl.Id == logger!.Id
            select rl;

        if (!await loggers.AnyAsync())
            return new CustomResponse("error", "Logger not found");

        var loggerDb = await loggers.FirstOrDefaultAsync();
        
        if (!BCrypt.Net.BCrypt.EnhancedVerify(logger!.Password, loggerDb!.Password))
            return new CustomResponse("error", "Logger password is incorrect");

        var logs = from l in _context.Logs
            where l.LoggerId == loggerDb.Id && l.Timestamp > DateTime.Now.AddDays(-1)
            select l;
        await logs.ForEachAsync(l =>
        {
            _context = new MyDbContext();
            l.LogInsides = _context.LogInsides.Where(li => li.LogId == l.Id).ToList();
        });
        await logs.ForEachAsync(l =>
        {
            _context = new MyDbContext();
            l.LogOutsides = _context.LogOutsides.Where(lo => lo.LogId == l.Id).ToList();
        });

        loggerDb!.Logs = logs.ToListAsync().Result.OrderByDescending(l => l.Timestamp).ToList();
        loggerDb.Locations = await _context.Locations.Where(l => l.LoggerId == loggerDb.Id).ToListAsync();

        return new CustomResponse("success", "Logger found", loggerDb);
    }
}