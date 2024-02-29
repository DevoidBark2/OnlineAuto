using System.ComponentModel.DataAnnotations;

namespace OnlineAuto.Models;

public class UserLoginRequest
{
    [Required,EmailAddress]
    public string email { get; set; } = string.Empty;

    [Required] 
    public string password { get; set; } = string.Empty;
}