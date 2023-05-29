using RadonAPI.Controllers;

namespace Tests;

public class LoggerTests
{
    [Test]
    public async Task Read()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "testid" },
        };

        LoggerController loggerController = new();

        await TestHandler.Run(body, Expect.Success, loggerController, loggerController.Read);
    }
}