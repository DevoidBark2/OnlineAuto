using System.ComponentModel.DataAnnotations;

namespace OnlineAuto.Models.Request;

public class DeleteCustomerOrderRequest
{
    [Required]
    public int orderId { get; set; }
    [Required]
    public int carrierId { get; set; }
}