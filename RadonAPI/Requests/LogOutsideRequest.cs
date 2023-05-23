using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Utils;

namespace RadonAPI.Requests;

public abstract class LogOutsideRequest
{
    public static LogOutside? Create(dynamic body, out CustomResponse? customResponse)
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

        customResponse = null;
        return new LogOutside
        {
            Temperature = double.Parse(body.temperature.ToString()),
            Humidity = double.Parse(body.humidity.ToString())
        };
    }
}