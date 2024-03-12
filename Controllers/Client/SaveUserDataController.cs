using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using OnlineAuto.Models.Request;

namespace OnlineAuto.Controllers;

[ApiController]
public class SaveUserDataController: ControllerBase
{
    [HttpPost("saveUserData")]
    public async Task<IActionResult> saveUserData(SaveUserDataRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var user = db.Users.FirstOrDefault(user => user.Id == request.id);
            if (user != null)
            {
                user.firstName = request.firstName; 
                user.secondName = request.secondName;
                user.email = request.email;
                user.phone = request.phone;

                db.Users.Update(user);
                await db.SaveChangesAsync();
                
                return Ok(new
                {
                    success = true,
                    message = "Данные пользователя успешно обновлены!",
                    userData = new
                    {
                        id = user.Id,
                        firstName = user.firstName,
                        phone = user.phone,
                        secondName = user.secondName,
                        email = user.email,
                        role = user.userRole
                    } 
                });
            }
            return NotFound("Пользователь не найден");
        }
    }
}