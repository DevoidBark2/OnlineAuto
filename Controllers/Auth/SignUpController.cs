using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers.Auth;

[ApiController]
public class SignUpController: ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(UserSignUpRequest request)
    {
        await using (var db = new ApplicationContext())
        {
            if (db.Users.Any(u => u.email == request.email))
            {
                return BadRequest("User already exists");
            }
            
            CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                firstName = request.firstName,
                secondName = request.secondName,
                email = request.email,
                phone = request.phone,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                userRole = request.userRole
            };

            db.Users.Add(user);
            db.SaveChanges();

            var responseUser = new
            {
                id = user.Id,
                firstName = user.firstName,
                phone = user.phone,
                secondName = user.secondName,
                email = user.email,
                role = user.userRole
            };
            
            return Ok(responseUser);
        }
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)
                .Concat(passwordSalt).ToArray());
        }
    }
}