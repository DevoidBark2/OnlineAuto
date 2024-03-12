using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;
using System.Threading.Tasks;

namespace OnlineAuto.Controllers
{
    [ApiController]
    public class GetUserDataController: ControllerBase
    {
        [HttpPost("getUserData")]
        public async Task<IActionResult> GetUserData(GetUserDataRequest request)
        {
            await using (var db = new ApplicationContext())
            {
                var orders = new List<Order>();

                if (request.roleUser == "logist")
                {
                    orders = db.Orders.Where(order => order.userId == request.UserId).ToList();
                }
                else
                {
                    orders = db.Orders.Where(order => order.customerId == request.UserId).ToList();
                }

                var userData = db.Users
                    .Where(user => user.Id == request.UserId)
                    .Select(user => new
                    {
                        firstName = user.firstName,
                        secondName = user.secondName,
                        email = user.email,
                        phone = user.phone,
                        userRole = user.userRole
                    })
                    .FirstOrDefault();
                
                if (userData != null)
                {
                    var response = new
                    {
                        ordersData = orders,
                        userData = userData
                    };
                
                    return Ok(response);
                }

                return BadRequest("Error");
            }
        }
    }
}