using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAuto.Models;

[Table("Orders")]
public class Order
{
    public int Id { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public int Price { get; set; }
    public DateTime Date { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int userId { get; set; }

    public int? customerId { get; set; } = 0;
}