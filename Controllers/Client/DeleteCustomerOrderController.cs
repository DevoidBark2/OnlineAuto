using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers;

[ApiController]
public class DeleteCustomerOrderController:ControllerBase
{
    private ClientService _clientService;

    public DeleteCustomerOrderController()
    {
        _clientService = new ClientService();
    }
    [HttpPost("deleteCustomerOrder")]
    public async Task<IActionResult> deleteCustomerOrder(DeleteCustomerOrderRequest request)
    {
        var orderRes = _clientService.DeleteCustomerOrder(request);

        return Ok(new
        {
            success = orderRes
        });
    }
}