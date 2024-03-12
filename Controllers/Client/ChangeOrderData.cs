using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;

namespace OnlineAuto.Controllers;

[ApiController]
public class ChangeOrderData:ControllerBase
{
    [HttpPost("changeOrderData")]
    public async Task<IActionResult> changeOrderData(OrderRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => order.userId == request.userId);

            Console.WriteLine(request.ToString());
            if (order != null)
            {
                order.From = request.pointOne;
                order.To = request.pointTwo;
                order.Comment = request.comment;
                order.Price = request.price;
                order.Date = request.date;
                db.SaveChanges();

                return Ok(new
                {
                    success = true,
                    message = "Заказ успешно обновлен!"
                });
            }
           
            return NotFound("Order not found for the specified userId.");
        }
    }
}