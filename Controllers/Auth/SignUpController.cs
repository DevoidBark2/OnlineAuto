using System;
using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Services.Auth;

namespace OnlineAuto.Controllers.Auth;

[ApiController]
public class SignUpController: ControllerBase
{

    private AuthService _authService;
    public SignUpController()
    {
        _authService = new AuthService();
    }
    [HttpPost("signup")]
    public async Task<IResult> SignUp(UserSignUpRequest request)
    {
        try
        {
            var user = _authService.Register(request);
            return Results.Ok(user);
        }
        catch (BadHttpRequestException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}