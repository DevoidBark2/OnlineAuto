using System.ComponentModel.DataAnnotations;

namespace OnlineAuto.Models.Request;

public class GetOrdersRequest
{
    [Required]
    public string From { get; set; }
    [Required]
    public string To { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public int UserId { get; set; }
    public string Comment { get; set; }
    
}