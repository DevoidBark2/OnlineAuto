using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers;

[ApiController]
public class ChangeOrderData:ControllerBase
{
    private ClientService _clientService;
    public ChangeOrderData()
    {
        _clientService = new ClientService();
    }
    [HttpPost("changeOrderData")]
    public async Task<IActionResult> changeOrderData(OrderRequest request)
    {

        var orderChange = _clientService.ChangeOrderData(request);

        return Ok(new
        {
            success = orderChange,
            message = orderChange ? "Заказ успешно обновлен!" : "произошла ошибка обновления заказа"
        });
    }
}