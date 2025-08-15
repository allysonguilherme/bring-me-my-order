using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _InventoryAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductFacade productFacade) : ControllerBase
{
    [HttpGet(Name = "GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var products = await productFacade.GetAllProducts();
        if (products.Any())
        {
            return Ok(products);
        }
        
        return NoContent();
    }
}