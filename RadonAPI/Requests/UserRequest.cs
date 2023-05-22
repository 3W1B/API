using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Utils;

namespace RadonAPI.Requests;

public abstract class UserRequest
{
    public static User? Register(dynamic data, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(data.firstName))
        {
            customResponse = new CustomResponse("error", "First name is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(data.lastName))
        {
            customResponse = new CustomResponse("error", "Last name is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(data.email))
        {
            customResponse = new CustomResponse("error", "Email is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(data.phone))
        {
            customResponse = new CustomResponse("error", "Phone number is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(data.password))
        {
            customResponse = new CustomResponse("error", "Password is null");
            return null;
        }
        
        customResponse = null;
        return new User
        {
            FirstName = data.firstName,
            LastName = data.lastName,
            Email = data.email,
            Phone = data.phone,
            Password = BCrypt.Net.BCrypt.EnhancedHashPassword(data.password.ToString()),
        };
    }
    
    public static User? Login(dynamic data, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(data.email))
        {
            customResponse = new CustomResponse("error", "Email is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(data.password))
        {
            customResponse = new CustomResponse("error", "Password is null");
            return null;
        }
        
        customResponse = null;
        return new User
        {
            Email = data.email,
            Password = data.password,
        };
    }
    
    public static dynamic? AddLogger(dynamic data, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(data.userId))
        {
            customResponse = new CustomResponse("error", "User id is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(data.radonLoggerId))
        {
            customResponse = new CustomResponse("error", "Logger id is null");
            return null;
        }
        
        if (ParameterHandler.IsNull(data.radonLoggerPassword))
        {
            customResponse = new CustomResponse("error", "Logger password is null");
            return null;
        }
        
        customResponse = null;
        return new
        {
            UserId = data.userId,
            RadonLoggerId = data.radonLoggerId,
            RadonLoggerPassword = data.radonLoggerPassword,
        };
    }
}