using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers;

[ApiController]
public class AddNewOrderController:ControllerBase
{
    private ClientService _clientService;
    public AddNewOrderController()
    {
        _clientService = new ClientService();
    }
    [HttpPost("addNewOrder")]
    public async Task<IActionResult> addNewOrder(AddNewOrderRequest request)
    {
        var orderRes = _clientService.AddNewOrder(request);

        return Ok(new
        {
            success = orderRes
        });
    }
}