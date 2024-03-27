using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuto.Models;
using OnlineAuto.Models.Response;
using OnlineAuto.Services.Auth;

namespace OnlineAuto.Controllers.Auth;

[ApiController]
public class LoginController: ControllerBase
{
    private readonly AuthService _authService;
    public LoginController()
    {
        _authService = new AuthService();
    }
    [HttpPost("login")]
    public async Task<IResult> Login(UserLoginRequest request)
    {
        try
        {
            return Results.Ok(_authService.Login(request));
        }
        catch (BadHttpRequestException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}