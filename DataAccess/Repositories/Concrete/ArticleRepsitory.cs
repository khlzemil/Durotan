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
    public class ArticleRepsitory : Repository<Article>, IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepsitory(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<Article>> GetOrderByAsync()
        {
            return await _context.Articles.OrderBy(lw => lw.Id).ToListAsync();
        }

        public async Task<List<Article>> GetTwoArticle()
        {
            return await _context.Articles.OrderBy(lw => lw.Id).Take(2).ToListAsync();
        }


        public async Task<int> GetPageCountAsync(int take)
        {

            var articlesCount = await _context.Articles.CountAsync();

            return (int)Math.Ceiling((decimal)articlesCount / take);

        }

        public async Task<List<Article>> PaginateBlogsAsync(int page, int take)
        {

            return await _context.Articles
                 .OrderByDescending(b => b.Id)
                 .Skip((page - 1) * take).Take(take)
                 .ToListAsync();

        }
    }
}
