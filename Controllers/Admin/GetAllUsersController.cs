using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class GetAllUsersController: ControllerBase
{
    [HttpGet("getALlData")]
    public async Task<IActionResult> getALlData()
    {
        using (var db = new ApplicationContext())
        {
            var selectedUsers = await db.Users
                .Where(user => user.firstName != null && user.secondName != null && user.email != null)
                .Select(user => new
                {
                    id = user.Id,
                    firstName = user.firstName,
                    secondName = user.secondName,
                    email = user.email,
                    role = user.userRole
                })
                .ToListAsync();

            var orders = db.Orders.ToList();

            return Ok(new
            {
                success = true,
                users = selectedUsers,
                orders = orders
            });
        }
    }
}