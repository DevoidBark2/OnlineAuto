using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class DeleteUserController:ControllerBase
{
    [HttpPost("deleteUser")]
    public async Task<IActionResult> DeleteUser(DeleteUserRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var user = db.Users.FirstOrDefault(user => user.Id == request.userId);

            if (user != null)
            {
                if (user.userRole == "logist")
                {
                    var userOrders = db.Orders.Where(order => order.userId == request.userId);
                    db.Orders.RemoveRange(userOrders);
                }

                if (user.userRole == "carrier")
                {
                    var userOrders = db.Orders.Where(order => order.userId == request.userId);
                    db.Orders.RemoveRange(userOrders);
                }
                
                db.Users.Remove(user);
                await db.SaveChangesAsync();

                return Ok(new { success = true });
            }

            return Ok(new
            {
                success = false
            });
        }
    }
}