using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers;

[ApiController]
public class GetAllOrdersController:ControllerBase
{
    [HttpGet("getAllOrders")]
    public async Task<IActionResult> getAllOrders()
    {
        using (var db = new ApplicationContext())
        {
            var orders = db.Orders.ToList();

            return Ok(new
            {
                success = true,
                orders = orders
            });
        }
    }
}