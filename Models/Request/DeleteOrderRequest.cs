namespace OnlineAuto.Models.Request;

public class DeleteOrderRequest
{
    public int userId { get; set; }
    public int orderId { get; set; }
}