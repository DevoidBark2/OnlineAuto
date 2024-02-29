using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;

namespace OnlineAuto.Controllers.Auth;

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

            CreatePasswordHash(request.password, out byte[] passwordHash,out byte[] passwordSalt );
            var user = new User
            {
                firstName = request.firstName,
                secondName = request.secondName,
                email = request.email,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                verificationToken = CreateToken(),
            };

            db.Users.Add(user);
            db.SaveChanges();
            
            //await SendConfirmationEmail(user.email, user.verificationToken);
            
            return Ok("User successfully created!");
        }
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private string CreateToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    } 
    
    //private async Task SendConfirmationEmail(string email, string verificationToken)
    //{
   //     await emailService.SendEmailAsync(email, "Подтвердите вашу регистрацию", $"Для завершения регистрации перейдите по ссылке: {verificationLink}");
    //}
}