using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Services.Admin;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class ChangeOrderController:ControllerBase
{
    private AdminService _adminService;

    public ChangeOrderController()
    {
        _adminService = new AdminService();
    }
    [HttpPost("changeOrderAdmin")]
    public async Task<IActionResult> ChangeOrderAdmin(ChangeOrderAdminRequest changeOrder)
    {
        var orderRes = _adminService.ChangeOrder(changeOrder);
        return Ok(new
        {
            success = orderRes
        });
    }
}