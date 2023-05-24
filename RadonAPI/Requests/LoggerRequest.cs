using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Utils;

namespace RadonAPI.Requests;

public abstract class LoggerRequest
{
    public static Logger? Read(dynamic body, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(body.id))
        {
            customResponse = new CustomResponse("error", "Logger id is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(body.password))
        {
            customResponse = new CustomResponse("error", "Logger password is null");
            return null;
        }

        customResponse = null;
        return new Logger
        {
            Id = body.id.ToString(),
            Password = body.password.ToString()
        };
    }
}