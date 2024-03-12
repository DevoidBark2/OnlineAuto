using System.ComponentModel.DataAnnotations;

namespace OnlineAuto.Models.Request;

public class GetUserDataRequest
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public string roleUser { get; set; }
}