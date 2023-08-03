using BookService.Core.Authors;
using BookService.Core.Authors.Abstractions;
using BookService.Core.Books;
using BookService.Core.Books.Abstractions;
using BookService.Core.GenreBook;
using BookService.Core.Genres;
using BookService.Core.Genres.Abstractions;
using BookService.Data;
using BookService.Data.EF.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceExtender.Data;
using ServiceExtender.Data.EF;
using ServiceExtender.Logging;

var builder = WebApplication.CreateBuilder(args);

_ = new ConectionsStrings(builder.Configuration.GetValue<string>(ConectionsStrings.NpgSqlKey));

builder.Services.AddDbContext<BookDbContext>(options =>
{
    options.UseNpgsql(ConectionsStrings.NpgSql);
    options.UseLazyLoadingProxies(true);
});


// Add services to the container.


builder.Services.AddTransient<IRepository<Book>,EFRepository<Book,BookModel,int>>(opt=> new EFRepository<Book, BookModel, int>
(opt.GetRequiredService<IContextCreator>(),
new System.Linq.Expressions.Expression<Func<BookModel, object>>[] { prop=>prop.Author})); 

builder.Services.AddTransient<IRepository<Genre>, EFRepository<Genre, GenreModel, int>>(opt => new EFRepository<Genre, GenreModel, int>
(opt.GetRequiredService<IContextCreator>()));


builder.Services.AddTransient<IRepository<Author>, EFRepository<Author, AuthorModel, int>>(opt => new EFRepository<Author, AuthorModel, int>
(opt.GetRequiredService<IContextCreator>(),
new System.Linq.Expressions.Expression<Func<AuthorModel, object>>[] { prop => prop.Books }));

builder.Services.AddTransient<IRepository<GenreBooks>, EFRepository<GenreBooks, GenreBookModel, int>>(opt =>
new EFRepository<GenreBooks, GenreBookModel, int>(opt.GetRequiredService<IContextCreator>(),
new System.Linq.Expressions.Expression<Func<GenreBookModel, object>>[] { prop => prop.Genre, prop => prop.Book }));

builder.Services.AddTransient<IContextCreator, ContextCreator>();
builder.Services.AddTransient<IBookService, DefaultBookService>();
builder.Services.AddTransient<IGenreService, DefaultGenreService>();
builder.Services.AddTransient<IAuthorService, DefaultAuthorService>();
builder.Services.AddSingleton(ILoggerService.GetService());
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("UserArea", policy => { policy.RequireRole("user"); });
});
builder.Services.AddControllersWithViews((x => { x.Conventions.Add(new UserAreaAutorization("User", "UserArea")); }));

builder.Services.AddMemoryCache();
builder.Services.AddSession();


//builder.Services.AddDefaultIdentity<User>(options =>
//{
//    options.User.RequireUniqueEmail = true;
//    options.Password.RequiredLength = 4;

//}).AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders().AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User>>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "myCompanyAuth";
    options.Cookie.HttpOnly = true;
    options.LoginPath = new PathString("/Account/Login");
    options.LogoutPath = new PathString("/Account/Logout");
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, (options) =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.LogoutPath = new PathString("/Account/Logout");
    });
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
} ).AddRoles<IdentityRole>().AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();


var app = builder.Build();

var env = app.Environment;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseCookiePolicy();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

});

UserDbContextInitializer.Initialize(app);

app.Run();