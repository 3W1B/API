using RadonAPI.Controllers;

namespace Tests;

public class RadonLoggerTests
{
    [Test]
    public async Task Read()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", 1 }
        };
        
        RadonLoggerController radonLoggerController = new();
        
        await TestHandler.Run(body, Expect.Success, radonLoggerController, radonLoggerController.Read);
    }
}