using System.Security.Cryptography;
using RadonAPI.Controllers;

namespace Tests;

public class LocationTests
{
    [Test]
    public async Task Update()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "loggerId", "testid" },
            { "loggerPassword", "testpassword"},
            { "latitude", RandomNumberGenerator.GetInt32(0, 100) + RandomNumberGenerator.GetInt32(0, 100) / 100.0 },
            { "longitude", RandomNumberGenerator.GetInt32(0, 100) + RandomNumberGenerator.GetInt32(0, 100) / 100.0 }
        };

        LocationController locationController = new();

        await TestHandler.Run(body, Expect.Success, locationController, locationController.Update);
    }
}