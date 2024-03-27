using System.Security.Cryptography;
using System.Text;
using OnlineAuto.Models;
using OnlineAuto.Models.Response;

namespace OnlineAuto.Services.Auth;

public class AuthService
{
    public UserResponse Login(UserLoginRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var email = request.email;
            var password = request.password;

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordHash = GetPasswordHash(passwordBytes);
            var user = db.Users.FirstOrDefault(user => user.email == email && user.passwordHash == passwordHash);

            if (user == null)
            {
                throw new BadHttpRequestException("Неверный логин или пароль");
            }
            
            var responseUser =  new UserResponse
            {
                id = user.Id,
                firstName = user.firstName,
                secondName = user.secondName,
                phone = user.phone,
                email = user.email,
                role = user.userRole
            };

            return responseUser;
        }
    }

    public UserResponse Register(UserSignUpRequest request)
    {
        using (var db = new ApplicationContext())
        {
            var email = request.email;
            var password = request.password;

            var userExist = db.Users.FirstOrDefault(user => user.email == email);
            if (userExist != null)
            {
                throw new BadHttpRequestException("Пользователь уже существует с таким email,повторите попытку!");
            }
            
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordHash = GetPasswordHash(passwordBytes);
            
            var user = new User
            {
                firstName = request.firstName,
                secondName = request.secondName,
                email = request.email,
                phone = request.phone,
                passwordHash = passwordHash,
                userRole = request.userRole
            };
            
            db.Users.Add(user);
            db.SaveChanges();

            var responseUser = new UserResponse
            {
                id = user.Id,
                firstName = user.firstName,
                secondName = user.secondName,
                email = user.email,
                phone = user.phone,
                role = user.userRole
            };
            
            return responseUser;
        }
    }
    
    private byte[] GetPasswordHash(byte[] input)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(input);
        }
    }
}