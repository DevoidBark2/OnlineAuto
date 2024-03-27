using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers;

[ApiController]
public class CreateOrderController: ControllerBase
{
    private ClientService _clientService;

    public CreateOrderController()
    {
        _clientService = new ClientService();
    }
    [HttpPost("createOrder")]
    public async Task<IActionResult> CreateOrder(OrderRequest request)
    {
        var order = _clientService.CreateOrder(request);
        return Ok(new
        {
            success = true,
            message = "Заказ успешно создан!",
            order = order
        });
    }
}