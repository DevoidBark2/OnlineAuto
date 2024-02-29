using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
[Route("[controller]")]
public class UsersController: ControllerBase
{
    [HttpGet]
    public IEnumerable<User> Get()
    {
        using (var db = new ApplicationContext())
        {
            return db.Users.ToList();
        }
    }
}