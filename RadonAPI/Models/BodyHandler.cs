using Newtonsoft.Json;

namespace RadonAPI.Models;

public abstract class Validator
{
    public static async Task<T?> Body<T>(Stream requestBody, Func<dynamic, dynamic> func) {
        var requestJson = await new StreamReader(requestBody).ReadToEndAsync();
        var body = JsonConvert.DeserializeObject(requestJson);
        
        T? value = func(body!);
        
        return value;
    }
}