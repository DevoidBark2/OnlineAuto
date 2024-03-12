using System.ComponentModel.DataAnnotations;

namespace OnlineAuto.Models.Request;

public class SaveUserDataRequest
{
    [Required]
    public int id { get; set; }
    [Required]
    public string firstName { get; set; }
    [Required]
    public string secondName { get; set; }
    [Required]
    public string phone { get; set; }
    [Required]
    public string email { get; set; }
}