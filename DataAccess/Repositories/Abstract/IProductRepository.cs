using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetAsync();
        Task<Product> GetWithPhotosAsync();
        Task<List<Product>> GetFirstFiveProductAsync();
        Task<List<Product>> GetFirstFourProductAsync();
        Task<List<ProductPhoto>> GetProductWithPhotosAsync(int id);
        Task<Product> GetProduct(int productId);

        IQueryable<Product> FilterByTitle(string title);

        Task<int> GetPageCountAsync(int take, string title);

        Task<List<Product>> Filter(string title, int page, int take);

        Task<List<Product>> PaginateProductsAsync(IQueryable<Product> products, int page, int take);

        Task<Product> GetWithTagsAsync(int id);
    }
}
