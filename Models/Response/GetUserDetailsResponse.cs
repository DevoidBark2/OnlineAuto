namespace OnlineAuto.Models.Response;

public class GetUserDetailsResponse
{
    public bool success { get; set; }
    public UserResponse? user { get; set; }
    public List<Order>? orders { get; set; }
}