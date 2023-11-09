﻿using Book_Store.Repository.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Connection DB
builder.Services.AddDbContext<DataContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("BookStore"))
    );

// Save cache
builder.Services.AddDistributedMemoryCache();

// Add Session
builder.Services.AddSession(
    options => {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.IsEssential = true;
        }
    );

/* ------------------------------------------------------------------------------------------------------------------------------ */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// Use Session
app.UseSession();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



// Admin

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
