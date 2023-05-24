using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Utils;

namespace RadonAPI.Requests;

public abstract class LocationRequest
{
    public static dynamic? Update(dynamic body, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(body.loggerId))
        {
            customResponse = new CustomResponse("error", "Logger id is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(body.loggerPassword))
        {
            customResponse = new CustomResponse("error", "Logger password is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(body.latitude))
        {
            customResponse = new CustomResponse("error", "Latitude is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(body.longitude))
        {
            customResponse = new CustomResponse("error", "Longitude is null");
            return null;
        }

        Location location = new()
        {
            LoggerId = body.loggerId.ToString(),
            Latitude = body.latitude,
            Longitude = body.longitude
        }; 
        
        customResponse = null;
        return new
        {
            Location = location,
            LoggerPassword = body.loggerPassword.ToString()
        };
    }
}