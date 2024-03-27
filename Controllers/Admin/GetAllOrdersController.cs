using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuto.Models;
using OnlineAuto.Services.Admin;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class GetAllOrdersController:ControllerBase
{
    private AdminService _adminService;

    public GetAllOrdersController()
    {
        _adminService = new AdminService();
    }
    [HttpGet("getOrders")]
    public async Task<IActionResult> getOrders()
    {

        var response = _adminService.GetAllOrders();

        return Ok(new
        {
            success = true,
            orders = response.orderData,
            users = response.userData
        });
    }
}