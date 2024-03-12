using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class GetAllOrdersController:ControllerBase
{
    [HttpGet("getOrders")]
    public async Task<IActionResult> getOrders()
    {
        using (var db = new ApplicationContext())
        {
            var orderDetails = await db.Orders.Include(order => order.User).ToListAsync();

            var selectedUsers = await db.Users.Where(user => user.userRole == "logist" || user.userRole == "carrier")
                .Select(user => new
                {
                    id = user.Id,
                    firstName = user.firstName,
                    secondName = user.secondName,
                    email = user.email,
                    role = user.userRole
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                orders = orderDetails,
                users = selectedUsers
            });
        }
    }
}