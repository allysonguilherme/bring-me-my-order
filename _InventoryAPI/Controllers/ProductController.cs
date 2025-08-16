using Application.DTOs;
using Application.Services.Interfaces;
using Business.Entities;
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

    [HttpGet("{id}", Name = "Get")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await productFacade.GetProduct(id);

        if (product is null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost(Name = "Create")]
    public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
    {
        createProductDto.Validate();
        if (!createProductDto.IsValid)
        {
            return BadRequest(createProductDto.Notifications);
        }

        var createProductResult = await productFacade.CreateProduct(new Product(
            createProductDto.Name,
            createProductDto.Stock,
            createProductDto.Price,
            createProductDto.Description));
         
        return createProductResult != null ? Ok("Produto criado com sucesso!") : NoContent();
    }
}