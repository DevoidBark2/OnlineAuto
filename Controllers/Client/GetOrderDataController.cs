using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers;

[ApiController]
public class GetOrderDataController: ControllerBase
{
    [HttpGet("getOrderData")]
    public async Task<IActionResult> GetOrderData([FromQuery] int orderId)
    {
        using (var db = new ApplicationContext())
        {
            var order = db.Orders.FirstOrDefault(order => order.Id == orderId);

            if (order == null)
            {
                return Ok(new
                {
                    success = false,
                    message = "Ошибка при получении данных заказа!"
                });
            }

            var user = db.Users.FirstOrDefault(user => user.Id == order.userId);

            if (user == null)
            {
                return Ok(new
                {
                    success = false,
                    message = "Пользователь не найден!"
                });
            }

            var userObject = new
            {
                id = user.Id,
                firstName = user.firstName,
                lastName = user.secondName,
                phoneNumber = user.phone
            };
            return Ok(new
            {
                success = true,
                order = order,
                user = userObject
            });
        }
    }
}