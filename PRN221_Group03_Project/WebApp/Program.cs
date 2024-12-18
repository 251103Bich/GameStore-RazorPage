using BussinessLogic.Interfaces;
using BussinessLogic.Service;
using DataAccess;
using DataAccess.DAOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Repository.Interfaces;
using Repository.Repositories;
using WebApp.Hubs;
using WebApp.Middleware;

var builder = WebApplication.CreateBuilder(args);


// Add scopes
builder.Services.AddScoped<ProfileDAO>();
builder.Services.AddScoped<GameDAO>();
builder.Services.AddScoped<CategoryDAO>();
builder.Services.AddScoped<OrderDAO>();
builder.Services.AddScoped<OrderDetailDAO>();
builder.Services.AddScoped<MoneyManagementDAO>();
builder.Services.AddScoped<FeedbackDAO>();
builder.Services.AddScoped<MoneyHistoryDAO>();

// Add repositories
builder.Services.AddScoped<IProfileRepository, ProfileRepository>(); 
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IMoneyManagementRepository, MoneyManagementRepository>();
builder.Services.AddScoped<IMoneyHistory, MoneyHistoryRepository>();

// Add services
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderSevice, OrderSevice>();
builder.Services.AddScoped<IOrderDetailSevice, OrderDetailSevice>();
builder.Services.AddScoped<IMoneyManagementSevice, MoneyManagementSevice>();
builder.Services.AddScoped<IMoneyHistoryService, MoneyHistoryService>();


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSignalR();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn mặc định
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();
app.UseMiddleware<CookieCheckMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();




app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/", (context) =>
{
    context.Response.Redirect("/Home/Index");
    return Task.CompletedTask;
});

app.MapHub<SignalrHub>("/signalrHub");

app.Run();
