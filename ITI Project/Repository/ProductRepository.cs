﻿using ITI_Project.Data;
using ITI_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_Project.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products
                                 .Include(p => p.Categories)   // لو عايز تجيب الكاتيجوري كمان
                                 .Include(p => p.Stock)      // لو عايز تجيب الستوك
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products
                                 .Include(p => p.Categories)
                                 .Include(p => p.Stock)
                                 .ToListAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
