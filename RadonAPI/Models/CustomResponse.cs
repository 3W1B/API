namespace RadonAPI.Models;

public class CustomResponse : Dictionary<string,dynamic>
{
    public CustomResponse(string status, string message, object? data = null)
    {
        Add("status", status);
        Add("message", message);
        Add("data", data ?? new object());
    }
}