using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;

namespace OnlineAuto.Controllers;

[ApiController]
public class DeleteOrder:ControllerBase
{
    [HttpPost("deleteOrder")]
    public async Task<IActionResult> deleteUser(DeleteOrderRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => order.Id == request.orderId && order.userId == request.userId);

            if (order == null)
            {
                return NotFound("Order not found");
            }
            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Order deleted successfully"
            });
        }
    }
}