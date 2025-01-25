using Microsoft.EntityFrameworkCore;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebUygulamaProje1.Utility.UygulamaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//_kitapTuruRepository  nesnesi => Dependency Injection
builder.Services.AddScoped<WebUygulamaProje1.Models.IKitapTuruRepository, WebUygulamaProje1.Models.KitapTuruRepository>();

//
builder.Services.AddScoped<IKitapRepository, KitapRepository>();



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

app.Run();
