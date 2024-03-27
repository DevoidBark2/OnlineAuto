using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Services.Admin;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class DeleteUserController:ControllerBase
{
    private AdminService _adminService;

    public DeleteUserController()
    {
        _adminService = new AdminService();
    }
    [HttpPost("deleteUser")]
    public async Task<IActionResult> DeleteUser(DeleteUserRequest request)
    {
        var response = _adminService.DeleteUser(request);

        return Ok(new
        {
            success = response
        });
    }
}