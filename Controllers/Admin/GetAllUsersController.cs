using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
public class GetAllUsersController: ControllerBase
{
    [HttpGet("getAllUsers")]
    public async Task<IActionResult> getALlUsers()
    {
        using (var db = new ApplicationContext())
        {
            var selectedUsers = await db.Users
                .Where(user => user.firstName != null && user.secondName != null && user.email != null) // Проверяем наличие NULL значений
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
                users = selectedUsers
            });
        }
    }
}