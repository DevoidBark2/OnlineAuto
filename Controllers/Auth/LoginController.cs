using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers.Auth;

[ApiController]
public class LoginController: ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        await using (var db = new ApplicationContext())
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.email == request.email);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            
            if (!VerifyPasswordHash(request.password, user.passwordHash,user.passwordSalt))
            {
                return BadRequest("Password or email is incorrect!");
            }

            var responseUser = new
            {
                id = user.Id,
                firstName = user.firstName,
                secondName = user.secondName,
                email = user.email,
                role = user.userRole
            };
            
            return Ok(responseUser);
        }
    }
    
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)
                .Concat(passwordSalt).ToArray());
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}