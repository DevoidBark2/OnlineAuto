using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers;

[ApiController]
public class GetOrderDataController: ControllerBase
{
    private ClientService _clientService;

    public GetOrderDataController()
    {
        _clientService = new ClientService();
    }
    [HttpGet("getOrderData")]
    public async Task<IResult> GetOrderData([FromQuery] int orderId)
    {

        var orderData = _clientService.GetOrderData(orderId);

        return Results.Ok(orderData);
    }
}