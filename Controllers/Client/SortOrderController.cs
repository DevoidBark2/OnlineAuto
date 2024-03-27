using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers;

[ApiController]
public class SortOrderController:ControllerBase
{

    private ClientService _clientService;

    public SortOrderController()
    {
        _clientService = new ClientService();
    }
    [HttpGet("sortOrders")]
    public async Task<IActionResult> sortOrders([FromQuery] string sortBy)
    {
        var orders = _clientService.SortOrder(sortBy);
        return Ok(new
        {
            success = true,
            orders = orders
        });
    }
}