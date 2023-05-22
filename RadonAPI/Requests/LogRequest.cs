using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Utils;

namespace RadonAPI.Requests;

public abstract class LogRequest
{
    public static Log? Create(dynamic body, out CustomResponse? customResponse, out LogInside? logInside, out LogOutside? logOutside)
    {
        if (ParameterHandler.IsNull(body.radonLoggerId))
        {
            customResponse = new CustomResponse("error", "Radon logger id is null");
            logInside = null;
            logOutside = null;
            return null;
        }
        
        if (ParameterHandler.IsNull(body.timestamp))
        {
            customResponse = new CustomResponse("error", "Timestamp is null");
            logInside = null;
            logOutside = null;
            return null;
        }
        
        if (ParameterHandler.IsNull(body.logInside))
        {
            customResponse = new CustomResponse("error", "Inside log is null");
            logInside = null;
            logOutside = null;
            return null;
        }
        
        if (ParameterHandler.IsNull(body.logOutside))
        {
            customResponse = new CustomResponse("error", "Outside log is null");
            logInside = null;
            logOutside = null;
            return null;
        }
        
        logInside = LogInsideRequest.Create(JsonConvert.DeserializeObject(body.logInside.ToString()), out customResponse);
        if (customResponse is not null)
        {
            logOutside = null;
            return null;
        }
        
        logOutside = LogOutsideRequest.Create(JsonConvert.DeserializeObject(body.logOutside.ToString()), out customResponse);
        if (customResponse is not null)
            return null;
        
        customResponse = null;
        var log  = new Log
        {
            RadonLoggerId = int.Parse(body.radonLoggerId.ToString()),
            Timestamp = body.timestamp,
        };
    
        return log;
    }
}