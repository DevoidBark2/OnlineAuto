using System.ComponentModel.DataAnnotations;

namespace OnlineAuto.Models.Request;

public class OrderRequest
{
    [Required] 
    public int Id { get; set; }
    public string pointOne { get; set; }
    public string pointTwo { get; set; }
    public int price { get; set; }
    public DateTime date { get; set; }
    public string comment { get; set; }
    public int userId { get; set; }
}