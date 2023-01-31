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
    public class FormRepository : Repository<Form>, IFormRepository
    {
        private readonly AppDbContext _context;
        public FormRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Form>> GetOrderForm()
        {
            return await _context.Forms.OrderBy(c => c.Status).ToListAsync();
        }
    }
}
