using Core.Entities;
using Core.Utilities.FileService;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AdminAbstractService = Web.Areas.Admin.Services.Abstract;
using AdminConcreteService = Web.Areas.Admin.Services.Concrete;
using Web.Services.Abstract;
using Web.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString, x => x.MigrationsAssembly("DataAccess")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    options.User.RequireUniqueEmail = true;

})
    .AddEntityFrameworkStores<AppDbContext>().
    AddDefaultTokenProviders();


#region Repositories

builder.Services.AddScoped<IHomeMainSliderRepository, HomeMainSliderRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
builder.Services.AddScoped<IOurJournalRepository, OurJournalRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepsitory>();
builder.Services.AddScoped<IStoryRepository, StoryRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductPhotoRepository, ProductPhotoRepository>();
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<IBasketProductRepository, BasketProductRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IProductTagRepository, ProductTagRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();


#endregion


#region Services
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IBasketService, BasketService>();


builder.Services.AddScoped<AdminAbstractService.IHomeMainSliderService, AdminConcreteService.HomeMainSliderService>();
builder.Services.AddScoped<AdminAbstractService.IBrandService, AdminConcreteService.BrandService>();
builder.Services.AddScoped<AdminAbstractService.IFeatureService, AdminConcreteService.FeatureService>();
builder.Services.AddScoped<AdminAbstractService.ICollectionService, AdminConcreteService.CollectionService>();
builder.Services.AddScoped<AdminAbstractService.IOurJournalService, AdminConcreteService.OurJournalService>();
builder.Services.AddScoped<AdminAbstractService.IArticleService, AdminConcreteService.ArticleService>();
builder.Services.AddScoped<AdminAbstractService.IAccountService, AdminConcreteService.AccountService>();
builder.Services.AddScoped<AdminAbstractService.IStoryService, AdminConcreteService.StoryService>();
builder.Services.AddScoped<AdminAbstractService.ICompanyService, AdminConcreteService.CompanyService>();
builder.Services.AddScoped<AdminAbstractService.IProductService, AdminConcreteService.ProductService>();
builder.Services.AddScoped<AdminAbstractService.IFormService, AdminConcreteService.FormService>();
builder.Services.AddScoped<AdminAbstractService.ITagService, AdminConcreteService.TagService>();
#endregion



#region Utilities
builder.Services.AddSingleton<IFileService, FileService>();
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=account}/{action=login}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

    var _configuration = scope.ServiceProvider.GetService<IConfiguration>();

    await DbInitializer.SeedAsync(userManager, roleManager, _configuration);
}


app.Run(); ;