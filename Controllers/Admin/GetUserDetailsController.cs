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
            Console.WriteLine("sad" + userDetails.firstName);
            
            if (userDetails != null)
            {
                if (userDetails.userRole == "logist")
                {
                   
                    var ordersLogist = db.Orders.Where(order => order.userId == userDetails.Id).ToList();
                    return Ok(new
                    {
                        success = true,
                        user = new
                        {
                            firstName = userDetails.firstName,
                            secondName = userDetails.secondName,
                            phone = userDetails.phone,
                            role = userDetails.userRole
                        },
                        orders = ordersLogist
                    });
                }

                if (userDetails.userRole == "carrier")
                {
                    var ordersCarrier = db.Orders.Where(order => order.customerId == userDetails.Id).ToList();
                    return Ok(new
                    {
                        success = true,
                        orders = ordersCarrier,
                        user = new
                        {
                            firstName = userDetails.firstName,
                            secondName = userDetails.secondName,
                            phone = userDetails.phone,
                            role = userDetails.userRole
                        },
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