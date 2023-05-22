using Newtonsoft.Json;
using RadonAPI.Controllers;
using RadonAPI.Entities;

namespace Tests;

public class LogTests
{
    [Test]
    public async Task Create()
    {
        var logOutside = new Dictionary<string, dynamic>
        {
            {"temperature", 1},
            {"humidity", 1}
        };
        var logInside = new Dictionary<string, dynamic>
        {
            {"temperature", 1},
            {"humidity", 1},
            {"radon", 1}
        };
        var body = new Dictionary<string, dynamic>
        {
            { "radonLoggerId", 1 },
            { "timestamp", DateTime.Now },
            { "logOutside", JsonConvert.SerializeObject(logOutside) },
            { "logInside", JsonConvert.SerializeObject(logInside) }
        };
        
        LogController logController = new();
        
        await TestHandler.Run(body, Expect.Success, logController, logController.Create);
    }
}