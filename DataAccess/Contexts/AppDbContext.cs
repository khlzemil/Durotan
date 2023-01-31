using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<HomeMainSlider> HomeMainSliders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Collection> Collections{ get; set; }
        public DbSet<OurJournal> OurJournals { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product>  Products{ get; set; }
        public DbSet<ProductPhoto>  ProductPhotos{ get; set; }
        public DbSet<Form> Forms{ get; set; }
        public DbSet<Basket> Baskets{ get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
    }
}
