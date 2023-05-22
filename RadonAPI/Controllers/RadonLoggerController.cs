using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadonAPI.Context;
using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Requests;

namespace RadonAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RadonLoggerController : ControllerBase
{
    private MyDbContext _context = new();
    
    [HttpPost]
    [Route(nameof(Read))]
    public async Task<CustomResponse> Read()
    {
        CustomResponse? customResponse = null;
        var radonLogger = await BodyHandler.Convert<RadonLogger>(Request.Body, d => RadonLoggerRequest.Read(d, out customResponse));
        if (customResponse is not null)
            return customResponse;

        var radonLoggers = from rl in _context.RadonLoggers
            where rl.Id == radonLogger!.Id
            select rl;
        
        if (!await radonLoggers.AnyAsync())
            return new CustomResponse("error", "Radon logger not found");
        
        var radonLoggerDb = await radonLoggers.FirstOrDefaultAsync();
        
        var logs = from l in _context.Logs
            where l.RadonLoggerId == radonLoggerDb.Id && l.Timestamp > DateTime.Now.AddDays(-1)
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
        
        radonLoggerDb!.Logs = logs.ToListAsync().Result.OrderByDescending(l => l.Timestamp).ToList();
        radonLoggerDb.Locations = await _context.Locations.Where(l => l.RadonLoggerId == radonLoggerDb.Id).ToListAsync();
        
        return new CustomResponse("success", "Radon logger found", radonLoggerDb);
    }
}