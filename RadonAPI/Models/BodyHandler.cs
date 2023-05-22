using Newtonsoft.Json;

namespace RadonAPI.Models;

public abstract class BodyHandler
{
    public static async Task<T?> Convert<T>(Stream requestBody, Func<dynamic, dynamic> func) {
        var requestJson = await new StreamReader(requestBody).ReadToEndAsync();
        var body = JsonConvert.DeserializeObject(requestJson);
        
        T? value = func(body!);
        
        return value;
    }
}