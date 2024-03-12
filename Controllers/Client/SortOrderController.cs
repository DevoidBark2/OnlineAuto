using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers;

[ApiController]
public class SortOrderController:ControllerBase
{
    [HttpGet("sortOrders")]
    public async Task<IActionResult> sortOrders([FromQuery] string sortBy)
    {
        using (var db = new ApplicationContext())
        {
            IQueryable<Order> orders = db.Orders;
            
            switch (sortBy)
            {
                case "price_high":
                    orders = orders.OrderBy(o => o.Price);
                    break;
                case "price_low":
                    orders = orders.OrderByDescending(o => o.Price);
                    break;
                case "date_high":
                    orders = orders.OrderBy(o => o.Date);
                    break;
                case "date_low":
                    orders = orders.OrderByDescending(o => o.Date);
                    break;
                default:
                    return BadRequest("Invalid sortBy parameter.");
            }

            var sortedOrders = orders.ToList();
            return Ok(new
            {
                success = true,
                orders = sortedOrders
            });
        }
    }
}