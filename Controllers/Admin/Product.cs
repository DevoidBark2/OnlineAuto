using Microsoft.AspNetCore.Mvc;
using OnlineAuto.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace OnlineAuto.Controllers.Admin;

[ApiController]
[Route("[controller]")]
public class ProductController: ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    
    // Получение всех продуктов
    [HttpGet]
    public IEnumerable<Product> Get()
    {
        using (var db = new ApplicationContext())
        {
            return db.Products.ToList();
        }
    }

    // Создание продукта
    [HttpPost]
    public Product Create([FromBody] Product product)
    {
        using (var db = new ApplicationContext())
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        return product;
    }

    // Изменение продукта
    [HttpPut]
    public IActionResult Change([FromBody] Product updatedProduct)
    {
        if (updatedProduct == null)
        {
            return BadRequest("Invalid product data.");
        }

        using (var db = new ApplicationContext())
        {
            var existingProduct = db.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);

            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }
            
            existingProduct.name = updatedProduct.name;
            existingProduct.price = updatedProduct.price;

            db.SaveChanges();
        }

        return Ok("Product updated successfully.");
    }

    // Удаление продукта
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using (var db = new ApplicationContext())
        {
            var existingProduct = db.Products.FirstOrDefault(p => p.Id == id);
        
            if (existingProduct == null)
            {
                return NotFound();
            }
        
            db.Products.Remove(existingProduct);
            db.SaveChanges();
            
            return NoContent(); 
        }
    }
}