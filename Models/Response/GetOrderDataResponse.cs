namespace OnlineAuto.Models.Response;

public class GetOrderDataResponse
{
    public bool success { get; set; }
    public Order? order { get; set; }
    public UserResponse? user { get; set; }
    public UserResponse? carrierUser { get; set; }
}