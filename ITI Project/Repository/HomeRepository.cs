using Humanizer.Localisation;
using ITI_Project.Data;
using ITI_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_Project.Repository
{
    public class HomeRepository:IHomeRepository
    {
        private readonly ApplicationDbContext context;
        public HomeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Category>> GetCategory()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Product>> DisplayProducts(string sTerm = "", int CategoryId = 0)
        {
            var productQuery = context.Products
               .AsNoTracking()
               .Include(x => x.Categories)
               .Include(x => x.Stock)
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(sTerm))
            {
                productQuery = productQuery.Where(b => b.ProductName.StartsWith(sTerm.ToLower()));
            }

            if (CategoryId > 0)
            {
                productQuery = productQuery.Where(b => b.CategoryId == CategoryId);
            }

            var product = await productQuery
                .AsNoTracking()
                .Select(product => new Product
                {
                    Id = product.Id,
                    Image = product.Image,
                    Description= product.Description,
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    CategoryName = product.Categories.CategoryName,
                    Quantity = product.Stock == null ? 0 : product.Stock.Quantity
                }).ToListAsync();

            return product;
        }
    }
}
