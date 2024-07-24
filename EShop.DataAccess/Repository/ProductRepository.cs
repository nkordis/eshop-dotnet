using EShop.DataAccess.Data;
using EShop.DataAccess.Repository.IRepository;
using EShop.Models.Models;

namespace EShop.DataAccess.Repository;

public class ProductRepository(ApplicationDbContext db) 
    : Repository<Product>(db), IProductRepository
{
    public void Update(Product product)
    {
        var productFromDb = db.Products.FirstOrDefault(p => p.Id == product.Id);
        if (productFromDb != null) 
        { 
            productFromDb.Title = product.Title;
            productFromDb.Description = product.Description;
            productFromDb.Size = product.Size;
            productFromDb.ListPrice = product.ListPrice;
            productFromDb.CategoryId = product.CategoryId;
            
            if(productFromDb.ImageUrl != null)
            {
                productFromDb.ImageUrl = product.ImageUrl;
            }
        }
    }
}
