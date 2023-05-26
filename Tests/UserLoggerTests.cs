using RadonAPI.Context;
using RadonAPI.Controllers;
using RadonAPI.Entities;

namespace Tests;

public class UserLoggerTests
{
    private readonly MyDbContext _context = new();
    
    [Test]
    public async Task Create()
    {
        User user = new()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = $"{Guid.NewGuid()}@gmail.com",
            Phone = $"{Guid.NewGuid()}",
            Password = $"{Guid.NewGuid()}"
        };
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        var body = new Dictionary<string, dynamic>
        {
            { "userId", user.Id },
            { "loggerId", "testid" },
            { "loggerPassword", "testpassword" }
        };

        UserLoggerController userLoggerController = new();

        await TestHandler.Run(body, Expect.Success, userLoggerController, userLoggerController.Create);
    }
}