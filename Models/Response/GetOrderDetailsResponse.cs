namespace OnlineAuto.Models.Response;

public class GetOrderDetailsResponse
{
    public bool success { get; set; }
    public Order? order { get; set; }
    public UserInfoResponse? customerOrderDetails { get; set; }
    public List<UserInfoResponse>? carrierOrderDetails { get; set; }
    
    public List<UserInfoResponse>? carrierUsers { get; set; }
}