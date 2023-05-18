using Newtonsoft.Json.Linq;
using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Utils;

namespace RadonAPI.Requests;

public abstract class LogInsideRequest
{
    
    
    public static LogInside? Create(dynamic body, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(body.temperature))
        {
            customResponse = new CustomResponse("error", "Temperature is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(body.humidity))
        {
            customResponse = new CustomResponse("error", "Humidity is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(body.radon))
        {
            customResponse = new CustomResponse("error", "Radon is null");
            return null;
        }
        
        customResponse = null;
        return new LogInside
        {
            Temperature = double.Parse(body.temperature.ToString()),
            Humidity = double.Parse(body.humidity.ToString()),
            Radon = double.Parse(body.radon.ToString())
        };
    }
}