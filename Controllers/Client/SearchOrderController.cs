using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers.Client;

[ApiController]
public class SearchOrderController:ControllerBase
{
    private ClientService _clientService;
    public SearchOrderController()
    {
        _clientService = new ClientService();
    }
    [HttpGet("searchOrders")]
    public async Task<IActionResult> SearchOrders([FromQuery(Name = "search")] string search = "")
    {
    
        var searchOrders = _clientService.SearchOrder(search);
        return Ok(new
        {
            orders = searchOrders
        });
    }
}