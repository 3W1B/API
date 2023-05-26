using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadonAPI.Context;
using RadonAPI.Entities;
using RadonAPI.Models;
using RadonAPI.Requests;

namespace RadonAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly MyDbContext _context = new();

    [HttpPost]
    [Route(nameof(Register))]
    public async Task<CustomResponse> Register()
    {
        CustomResponse? customResponse = null;
        var user = await BodyHandler.Convert<User>(Request.Body, d => UserRequest.Register(d, out customResponse));
        if (customResponse is not null)
            return customResponse;

        var users = from u in _context.Users
            where u.Email == user!.Email || u.Phone == user.Phone
            select u;

        if (await users.AnyAsync())
            return new CustomResponse("error", "User with this email or phone already exists");

        await _context.Users.AddAsync(user!);
        await _context.SaveChangesAsync();

        return new CustomResponse("success", "User created", user);
    }

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<CustomResponse> Login()
    {
        CustomResponse? customResponse = null;
        var user = await BodyHandler.Convert<User>(Request.Body, d => UserRequest.Login(d, out customResponse));
        if (customResponse is not null)
            return customResponse;

        var users = from u in _context.Users
            where u.Email == user!.Email
            select u;

        if (!await users.AnyAsync())
            return new CustomResponse("error", "User with this email does not exist");

        var userDb = await users.FirstOrDefaultAsync();
        userDb!.UserLoggers = await _context.UserLoggers.Where(u => u.UserId == userDb.Id).ToListAsync();

        return BCrypt.Net.BCrypt.EnhancedVerify(user!.Password, userDb.Password)
            ? new CustomResponse("success", "User logged in", userDb)
            : new CustomResponse("error", "Password is incorrect");
    }
}