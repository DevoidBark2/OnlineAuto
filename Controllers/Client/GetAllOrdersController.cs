using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers;

[ApiController]
public class GetAllOrdersController:ControllerBase
{
    private ClientService _clientService;

    public GetAllOrdersController()
    {
        _clientService = new ClientService();
    }
    [HttpGet("getAllOrders")]
    public async Task<IActionResult> getAllOrders()
    {
        var orders = _clientService.GetAllOrder();
        return Ok(new
        {
            success = true,
            orders = orders
        });
    }
}