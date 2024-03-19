using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class ChangeOrderController:ControllerBase
{
    [HttpPost("changeOrderAdmin")]
    public async Task<IActionResult> ChangeOrderAdmin(ChangeOrderAdminRequest changeOrderAdminRequest)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => order.Id == changeOrderAdminRequest.orderId);

            if (order != null)
            {
                order.customerId = changeOrderAdminRequest.carrierId;
                db.Orders.Update(order);
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