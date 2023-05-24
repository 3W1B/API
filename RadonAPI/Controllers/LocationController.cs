using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadonAPI.Context;
using RadonAPI.Models;
using RadonAPI.Requests;

namespace RadonAPI.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly MyDbContext _context = new();
    
    [HttpPost]
    [Route(nameof(Update))]
    public async Task<CustomResponse> Update()
    {
        CustomResponse? customResponse = null;
        var d = await BodyHandler.Convert<dynamic>(Request.Body, d => LocationRequest.Update(d, out customResponse));
        if (customResponse is not null)
            return customResponse;
        
        string loggerId = d!.Location.LoggerId;
        var dbLogger = await _context.Loggers.FindAsync(loggerId);
        if (dbLogger is null)
            return new CustomResponse("error", "Logger does not exist");
        
        if (!BCrypt.Net.BCrypt.EnhancedVerify(d.LoggerPassword, dbLogger.Password))
            return new CustomResponse("error", "Logger password is incorrect");

        var dbLocation = await _context.Locations.Where(l => l.LoggerId!.Equals(loggerId)).FirstOrDefaultAsync();
        if (dbLocation is null)
        {
            await _context.Locations.AddAsync(d.Location);
            await _context.SaveChangesAsync();
            return new CustomResponse("success", "Location created", d.Location);
        }
        
        dbLocation.Latitude = d.Location.Latitude;
        dbLocation.Longitude = d.Location.Longitude;
        
        await _context.SaveChangesAsync();
        
        return new CustomResponse("success", "Location updated", dbLocation);
    }
}