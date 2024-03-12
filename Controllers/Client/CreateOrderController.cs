using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;

namespace OnlineAuto.Controllers;

[ApiController]
public class CreateOrderController: ControllerBase
{
    [HttpPost("createOrder")]
    public async Task<IActionResult> saveUserData(OrderRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var order = new Order
            {
                From = request.pointOne,
                To = request.pointTwo,
                Price = request.price,
                Date = request.date,
                Comment = request.comment,
                userId = request.userId,
            };

            db.Orders.Add(order);
            db.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Заказ успешно создан!",
                order = order
            });
        }
    }
}