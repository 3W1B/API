using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Utils;

namespace RadonAPI.Requests;

public abstract class RadonLoggerRequest
{
    public static RadonLogger? Read(dynamic body, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(body.id))
        {
            customResponse = new CustomResponse("error", "RadonLogger id is null");
            return null;
        }
        
        customResponse = null;
        return new RadonLogger
        {
            Id = int.Parse(body.id.ToString())
        };
    }
}