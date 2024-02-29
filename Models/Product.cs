using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;
namespace OnlineAuto.Models;

[Table("Products")]
public class Product
{
    public int Id { get; set; }
    public string name { get; set; }
    public double price { get; set; }
    public double discountPrice { get; set; }
    
    [SwaggerSchema(Description = "Ссылка на изображение товара")]
    public string image { get; set; }
    public string description { get; set; }
}