using Microsoft.EntityFrameworkCore;
using TechStoreInventory.Data;
using TechStoreInventory.Models;

namespace TechStoreInventory.Services;

public class ProductService(AppDbContext dbContext)
{
    public async Task<List<dynamic>> GetAllProductsWithCategoryAsync()
    {
        return await dbContext.Products
            .Include(p => p.Category)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Quantity,
                p.CategoryId,
                CategoryName = p.Category!.Name
            })
            .ToListAsync<dynamic>();
    }
    
    public async Task<decimal> GetInventoryTotalValueAsync()
    {
        return await dbContext.Products
            .AsNoTracking()
            .SumAsync(p => p.Price * p.Quantity);
    }
    
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await dbContext.Products.FindAsync(id);
    }
    
    public async Task<Product> CreateProductAsync(Product product)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        return product;
    }
    
    public async Task<Product?> UpdateProductAsync(int id, Product product)
    {
        var existingProduct = await dbContext.Products.FindAsync(id);
        if (existingProduct == null) return null;
        
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;
        existingProduct.CategoryId = product.CategoryId;
        
        await dbContext.SaveChangesAsync();
        return existingProduct;
    }
    
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product == null) return false;
        
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
        return true;
    }
}