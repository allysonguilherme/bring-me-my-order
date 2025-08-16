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

    [HttpPut("{id}/AddStock", Name = "AddStock")]
    public async Task<IActionResult> AddStock(int id, [FromBody] UpdateProductStockDto updateProductStockDto)
    {
        
        updateProductStockDto.Validate();
        if (!updateProductStockDto.IsValid)
        {
            return BadRequest(updateProductStockDto.Notifications);
        }
        
        var product = await productFacade.GetProduct(id);

        if (product is null)
        {
            return NotFound("Product not found");
        }
        
        var (success, message) = await productFacade.AddProductStock(product, updateProductStockDto.Quantity);
        
        return success ? Ok(message) : BadRequest(message);
    }

    [HttpPut("{id}/WithdrawStock", Name = "WithdrawStock")]
    public async Task<IActionResult> WithdrawStock(int id, [FromBody] UpdateProductStockDto updateProductStockDto)
    {
        updateProductStockDto.Validate();
        if (!updateProductStockDto.IsValid)
        {
            return BadRequest(updateProductStockDto.Notifications);
        }
        
        var product = await productFacade.GetProduct(id);

        if (product is null)
        {
            return NotFound("Product not found");
        }
        
        var (success, message) = await productFacade.WithdrawProductStock(product, updateProductStockDto.Quantity);
        
        return success ? Ok(message) : BadRequest(message);
    }
}