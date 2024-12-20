using Microsoft.AspNetCore.Mvc;
using FinalTest.Modles;
using FinalTest.Modles.DTOs;

namespace FinalTest.Controllers
{
    [Route("/products")]
    public class ProductController : Controller
    {
        private readonly List<Product> Products = new List<Product>();

        [HttpGet("/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            return Ok();
        }
        [HttpGet("/GetAll")]
        public IActionResult GetAllProduct()
        {
            return Ok(Products.ToList());
        }
        [HttpPost]
        public IActionResult AddProduct(ProductInputDTO productDTO)
        {
            if (productDTO.Price <= 0)
                return BadRequest("Invalid price.");
            var usedName = Products.Any(p=>p.Name == productDTO.Name);
            if (usedName)
                return BadRequest("Invalid name");
            var newProduct = new Product(Products.Count(), productDTO.Name, productDTO.Price);            
            Products.Add(newProduct);
            return Ok(newProduct);
        }
    }
}
