using Newtonsoft.Json;
using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Utils;

namespace RadonAPI.Requests;

public abstract class LogRequest
{
    public static dynamic? Create(dynamic body, out CustomResponse? customResponse)
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

        if (ParameterHandler.IsNull(body.timestamp))
        {
            customResponse = new CustomResponse("error", "Timestamp is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.logInside))
        {
            customResponse = new CustomResponse("error", "Inside log is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.logOutside))
        {
            customResponse = new CustomResponse("error", "Outside log is null");
            return null;
        }

        var logInside = LogInsideRequest.Create(JsonConvert.DeserializeObject(body.logInside.ToString()), out customResponse);
        if (customResponse is not null)
            return null;

        var logOutside = LogOutsideRequest.Create(JsonConvert.DeserializeObject(body.logOutside.ToString()), out customResponse);
        if (customResponse is not null)
            return null;

        customResponse = null;
        
        Log log = new()
        {
            LoggerId = body.loggerId.ToString(),
            Timestamp = body.timestamp
        };
        
        log.LogInsides.Add(logInside!);
        log.LogOutsides.Add(logOutside!);

        return new
        {
            Log = log,
            LoggerPassword = body.loggerPassword.ToString()
        };
    }
}