using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;

namespace OnlineAuto.Controllers;

[ApiController]
public class DeleteCustomerOrderController:ControllerBase
{
    [HttpPost("deleteCustomerOrder")]
    public async Task<IActionResult> deleteCustomerOrder(DeleteCustomerOrderRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => 
                order.Id == request.orderId && 
                order.customerId == request.carrierId
                );

            if (order != null)
            {
                order.customerId = 0;
                db.SaveChanges();

                return Ok(new
                {
                    success = true
                });
            }

            return Ok(new
            {
                success = false
            });
        }
    }
}