using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Services.Admin;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class GetUserDetailsController:ControllerBase
{
    private AdminService _adminService;

    public GetUserDetailsController()
    {
        _adminService = new AdminService();
    }
    [HttpGet("getUserDetails")]
    public async Task<IResult> GetUserDetails([FromQuery] int userId)
    {
        var user = _adminService.GetUserDetails(userId);

        return Results.Ok(user);
    }
}