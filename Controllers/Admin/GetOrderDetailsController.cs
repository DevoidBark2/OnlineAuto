using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Response;
using OnlineAuto.Services.Admin;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class GetOrderDetailsController : ControllerBase
{
    private AdminService _adminService;
    public GetOrderDetailsController()
    {
        _adminService = new AdminService();
    }
    [HttpGet("getOrderDetails")]
    public async Task<IResult> GetOrderDetails([FromQuery] int orderId)
    {
        var order = _adminService.GetOrderDetails(orderId);

        return Results.Ok(order);
    }
}