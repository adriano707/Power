using Microsoft.EntityFrameworkCore;
using Power.Data;
using Power.Data.Repositories;
using Power.Domain.Repositories;
using Power.Domain.RingsAggregate.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PowerDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("app"));
});

builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddScoped<IRingService, RingService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});

//app.MapRazorPages();

app.Run();