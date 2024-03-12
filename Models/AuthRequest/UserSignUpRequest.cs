using System.ComponentModel.DataAnnotations;

namespace OnlineAuto.Models;

public class UserSignUpRequest
{
    [Required]
    public string firstName { get; set; } = string.Empty;
    
    [Required]
    public string secondName { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string email { get; set; } = string.Empty;

    [Required] 
    public string phone { get; set; } = string.Empty;

    [Required, MinLength(8)] 
    public string password { get; set; } = string.Empty;

    [Required] 
    public string userRole { get; set; } = string.Empty;
}