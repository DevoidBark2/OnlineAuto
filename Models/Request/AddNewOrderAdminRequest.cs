namespace OnlineAuto.Models.Request;

public class AddNewOrderRequestAdmin
{
    public string pointOne { get; set; }
    public string pointTwo { get; set; }
    public int price { get; set; }
    public DateTime date { get; set; }
    public string comment { get; set; }
    public int customerId { get; set; }
    public int? carrierId { get; set; }
}