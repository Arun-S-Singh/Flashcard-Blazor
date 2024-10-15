using Flashcard.Areas.Identity;
using Flashcard.Data;
using Flashcard.Data.Card;
using Flashcard.Data.Subscription;
using Flashcard.Data.Cart;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Flashcard.Data.Bundle;
using Serilog;
using Flashcard.Data.Order;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(configuration)
    .CreateLogger();



// DB context 
var connectionString = builder.Configuration.GetConnectionString("MemorizeDB");
builder.Services.AddDbContextFactory<FlashcardDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// Identity 
builder.Services.AddDefaultIdentity<FlashcardUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FlashcardDBContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<FlashcardUser>>();


// Adding application services to the container
builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<BundleService>();
builder.Services.AddScoped<SubscriptionService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<OrderService>();

//builder.Services.AddSerilog();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


try 
{
    Log.Information("Application Starting up.");
    app.Run();
} 
catch (Exception ex)
{
    Log.Fatal(ex,"Application failed to start");
} 
finally 
{
    Log.CloseAndFlush();
}

