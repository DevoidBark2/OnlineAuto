using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAuto.Models;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    public string firstName { get; set; }
    public string secondName { get; set; }
    public string email { get; set; }
    
    public string phone { get; set; }
    public byte[] passwordHash { get; set; } = new byte[32];
    public byte[] passwordSalt { get; set; } = new byte[32];
    public string userRole { get; set; }
}