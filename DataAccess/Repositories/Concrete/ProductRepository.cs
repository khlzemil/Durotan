using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Product> GetAsync()
        {
            return await _context.Products.FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetFirstFiveProductAsync()
        {
            return await _context.Products.OrderByDescending(p => p.Id).Take(5).ToListAsync();
        }

        public async Task<List<Product>> GetFirstFourProductAsync()
        {
            return await _context.Products.OrderByDescending(p => p.Id).Take(4).ToListAsync();
        }

        public async Task<List<ProductPhoto>> GetProductWithPhotosAsync(int id)
        {
            return await _context.ProductPhotos.Where(p => p.ProductId == id).ToListAsync();
        }

        public async Task<int> GetPageCountAsync(int take, string title)
        {
            var products = FilterByTitle(title);
            var pagecount = await products.CountAsync();
            return (int)Math.Ceiling((decimal)pagecount / take);

        }

        public async Task<Product> GetWithPhotosAsync()
        {
            return await _context.Products.Include(p => p.ProductPhotos).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProduct(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            return product;
        }

        public async Task<Product> GetWithTagsAsync(int id)
        {
            return await _context.Products
                .Include(c => c.ProductTags)
                .ThenInclude(ct => ct.Tag)
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<List<Product>> Filter(string title, int page, int take)
        {
            var products = FilterByTitle(title);
            return await PaginateProductsAsync(products, page, take);
        }


        public IQueryable<Product> FilterByTitle(string title)
        {
            return _context.Products.Where(p => !string.IsNullOrEmpty(title) ? p.Name.Contains(title) : true);
        }

        public async Task<List<Product>> PaginateProductsAsync(IQueryable<Product> products, int page, int take)
        {
            return await products
                 .OrderByDescending(b => b.Id)
                 .Skip((page - 1) * take).Take(take)
                 .ToListAsync();
        }

    }
}
