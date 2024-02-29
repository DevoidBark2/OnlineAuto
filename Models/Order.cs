using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAuto.Models;

[Table("Orders")]
public class Order
{
    public int Id { get; set; }
}