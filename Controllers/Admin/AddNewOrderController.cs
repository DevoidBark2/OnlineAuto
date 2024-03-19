using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;

namespace OnlineAuto.Controllers.Admin;
[ApiController]
public class AddNewOrderController:ControllerBase
{
    [HttpPost("addNewOrderAdmin")]
    public async Task<IActionResult> AddNewOrder(AddNewOrderRequestAdmin orderRequest)
    {
        using (var db = new ApplicationContext())
        {
            var order = new Order
            {
                From = orderRequest.pointOne,
                To = orderRequest.pointTwo,
                Price = orderRequest.price,
                Date = orderRequest.date,
                Comment = orderRequest.comment,
                userId = orderRequest.customerId,
                customerId = orderRequest.carrierId
            };

            db.Orders.Add(order);
            db.SaveChanges();

            return Ok(new
            {
                success = true
            });
        }
    }
}