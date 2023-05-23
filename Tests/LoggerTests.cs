using RadonAPI.Controllers;

namespace Tests;

public class LoggerTests
{
    private readonly string _randomId = $"{Guid.NewGuid()}";
    private readonly string _randomPassword = $"{Guid.NewGuid()}";
    
    [Test]
    public async Task Create()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", _randomId },
            { "password", _randomPassword }
        };
        
        LoggerController loggerController = new();
        
        await TestHandler.Run(body, Expect.Success, loggerController, loggerController.Create);
    }
    
    [Test]
    public async Task Read()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "testid" }
        };

        LoggerController loggerController = new();

        await TestHandler.Run(body, Expect.Success, loggerController, loggerController.Read);
    }
}