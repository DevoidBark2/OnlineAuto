using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using OnlineAuto.Services.Client;

namespace OnlineAuto.Controllers;

[ApiController]
public class DeleteOrder:ControllerBase
{
    private ClientService _clientService;
    public DeleteOrder()
    {
        _clientService = new ClientService();
    }
    [HttpPost("deleteOrder")]
    public async Task<IActionResult> deleteUser(DeleteOrderRequest request)
    {
        var orderDelete = _clientService.DeleteOrder(request);
        
        return Ok(new
        {
            success = orderDelete,
            message = orderDelete ? "Order deleted successfully" : "Order delete error!"
        });
        
    }
}