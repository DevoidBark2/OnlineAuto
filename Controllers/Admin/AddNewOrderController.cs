using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Services.Admin;

namespace OnlineAuto.Controllers.Admin;
[ApiController]
public class AddNewOrderController:ControllerBase
{
    private AdminService _adminService;

    public AddNewOrderController()
    {
        _adminService = new AdminService();
    }
    [HttpPost("addNewOrderAdmin")]
    public async Task<IActionResult> AddNewOrder(AddNewOrderRequestAdmin orderRequest)
    {
        var order = _adminService.AddNewOrder(orderRequest);

        return Ok(new
        {
            success = order
        });
    }
}