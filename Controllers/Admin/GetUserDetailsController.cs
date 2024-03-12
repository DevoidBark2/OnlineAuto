using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class GetUserDetailsController:ControllerBase
{
    [HttpGet("getUserDetails")]
    public async Task<IActionResult> GetUserDetails([FromQuery] int userId)
    {
        using (var db = new ApplicationContext())
        {
            Console.WriteLine(userId);
            var userDetails = db.Users.FirstOrDefault(user => user.Id == userId);
            Console.WriteLine("sad" + userDetails);
            
            if (userDetails != null)
            {
                if (userDetails.userRole == "logist")
                {
                   
                    var ordersLogist = db.Orders.FirstOrDefault(order => order.userId == userDetails.Id);
                    return Ok(new
                    {
                        success = true,
                        user = userDetails,
                        orders = ordersLogist
                    });
                }

                if (userDetails.userRole == "carrier")
                {
                    var ordersCarrier = db.Orders.FirstOrDefault(order => order.customerId == userDetails.Id);
                    return Ok(new
                    {
                        success = true,
                        orders = ordersCarrier,
                        user = userDetails
                    });
                }

                return Ok(new
                {
                    success = false
                });
            }

            return Ok(new
            {
                success = false
            });
        }
    }
}