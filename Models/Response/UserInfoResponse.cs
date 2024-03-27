namespace OnlineAuto.Models.Response;

public class UserInfoResponse
{
    public int Id { get; set; }
    public string firstName { get; set; }
    public string secondName { get; set; }
    public string role { get; set; }
    public int? customerId { get; set; }
    public string? phone { get; set; }
}