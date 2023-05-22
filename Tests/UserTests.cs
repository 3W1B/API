﻿using RadonAPI.Context;
using RadonAPI.Controllers;

namespace Tests;

public class UserTests
{
    private readonly string _randomEmail = $"{Guid.NewGuid()}@gmail.com";
    private readonly string _randomPhone = $"{Guid.NewGuid()}";
    private readonly string _randomPassword = $"{Guid.NewGuid()}";
    
    private int _userId;
    
    private readonly MyDbContext _context = new();
    
    
    [Test]
    [Order(1)]
    public async Task Register()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "firstName", "John" },
            { "lastName", "Doe" },
            { "email", _randomEmail },
            { "phone", _randomPhone },
            { "password", _randomPassword }
        };
        
        UserController userController = new();
        
        await TestHandler.Run(body, Expect.Success, userController, userController.Register);
    }
    
    [Test]
    [Order(2)]
    public async Task Login()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "email", _randomEmail },
            { "password", _randomPassword }
        };
        
        UserController userController = new();

        await TestHandler.Run(body, Expect.Success, userController, userController.Login);
        
        _userId = _context.Users.FirstOrDefault(u => u.Email == _randomEmail)!.Id;
    }
    
    [Test]
    [Order(3)]
    public async Task AddLogger()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "userId", _userId },
            { "radonLoggerId", 1 },
            { "radonLoggerPassword", "testpassword" }
        };
        
        UserController userController = new();
        
        await TestHandler.Run(body, Expect.Success, userController, userController.AddLogger);
    }
}