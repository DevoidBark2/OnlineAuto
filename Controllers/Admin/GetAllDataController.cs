using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Services.Admin;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class GetAllDataController: ControllerBase
{
    private AdminService _adminService;

    public GetAllDataController()
    {
        _adminService = new AdminService();
    }
    [HttpGet("getALlData")]
    public async Task<IActionResult> getALlData()
    {
        var response = _adminService.getALlData();
        return Ok(new
        {
            success = true,
            users = response.userData,
            orders = response.orderData
        });
    }
}