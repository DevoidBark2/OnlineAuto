using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;

namespace OnlineAuto.Controllers;

[ApiController]
public class AddNewOrderController:ControllerBase
{
    [HttpPost("addNewOrder")]
    public async Task<IActionResult> addNewOrder(AddNewOrderRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => order.Id == request.orderId);

            if (order == null)
            {
                return Ok(new
                {
                    success = false
                });
            }

            order.customerId = request.customerId;
            db.Orders.Update(order);

            db.SaveChanges();

            return Ok(new
            {
                success = true
            });
        }
    }
}