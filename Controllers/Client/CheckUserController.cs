using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;

namespace OnlineAuto.Controllers;

[ApiController]
public class CheckUserController:ControllerBase
{
    [HttpPost("checkUser")]
    public async Task<IActionResult> CheckUser(CheckUserRequest checkUserRequest)
    {
        using (var db = new ApplicationContext())
        {
            var user = db.Users.FirstOrDefault(user => user.Id == checkUserRequest.userId);

            if (user != null)
            {
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