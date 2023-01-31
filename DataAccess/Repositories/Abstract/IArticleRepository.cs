using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IArticleRepository : IRepository<Article>
    {
       Task<List<Article>> GetOrderByAsync();
        Task<List<Article>> GetTwoArticle();

        Task<List<Article>> PaginateBlogsAsync(int page, int take);

        Task<int> GetPageCountAsync(int take);
    }
}
