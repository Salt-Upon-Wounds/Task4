using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task4.Data;
using Task4.Areas.Identity.Data;
using Task4.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ContextConnection") ?? throw new InvalidOperationException("Connection string 'ContextConnection' not found.");

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => { 
    options.SignIn.RequireConfirmedAccount = false;
    options.Password = new PasswordOptions
    {
        RequireDigit = false,
        RequiredLength = 6,
        RequireLowercase = false,
        RequireNonAlphanumeric = false,
        RequireUppercase = false
    };
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<Context>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
