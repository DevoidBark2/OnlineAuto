using System.ComponentModel.DataAnnotations;

namespace OnlineAuto.Models.Request;

public class DeleteUserRequest
{
    [Required]
    public int userId { get; set; }
}