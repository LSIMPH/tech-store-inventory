using Microsoft.AspNetCore.Mvc;
using TechStoreInventory.Models;
using TechStoreInventory.Services;

namespace TechStoreInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetAllProductsWithCategoryAsync();
        return Ok(products);
    }
    
    [HttpGet("inventory-value")]
    public async Task<IActionResult> GetInventoryValue()
    {
        var totalValue = await productService.GetInventoryTotalValueAsync();
        return Ok(new { totalValue });
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        if (product.Price <= 0 || product.Quantity <= 0)
            return BadRequest("Giá và số lượng phải lớn hơn 0");
        
        var created = await productService.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        if (product.Price <= 0 || product.Quantity <= 0)
            return BadRequest("Giá và số lượng phải lớn hơn 0");
        
        var updated = await productService.UpdateProductAsync(id, product);
        if (updated == null) return NotFound();
        return Ok(updated);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await productService.DeleteProductAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}