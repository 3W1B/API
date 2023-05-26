using RadonAPI.Models;
using RadonAPI.Utils;

namespace RadonAPI.Requests;

public abstract class UserLoggerRequest
{
    public static dynamic? Create(dynamic data, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(data.userId))
        {
            customResponse = new CustomResponse("error", "User id is null");
            return null;
        }

        if (ParameterHandler.IsNull(data.loggerId))
        {
            customResponse = new CustomResponse("error", "Logger id is null");
            return null;
        }

        if (ParameterHandler.IsNull(data.loggerPassword))
        {
            customResponse = new CustomResponse("error", "Logger password is null");
            return null;
        }

        customResponse = null;
        return new
        {
            UserId = data.userId,
            LoggerId = data.loggerId,
            LoggerPassword = data.loggerPassword
        };
    }
}