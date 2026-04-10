using Microsoft.AspNetCore.Mvc;
using TechStoreInventory.Data;

namespace TechStoreInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await dbContext.Categories.ToListAsync();
        return Ok(categories);
    }
}