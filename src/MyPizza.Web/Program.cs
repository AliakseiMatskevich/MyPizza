using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyPizza.Infrastructure.Data;
using MyPizza.Infrastructure.Data.Identity;
using MyPizza.Web.Configuration;
using MyPizza.Web.Services.EmailSender;
using Serilog;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

#region Configure AppDbContext
builder.Services.AddDbContext<PizzaContext>(context =>
        context.UseSqlServer(builder.Configuration.GetConnectionString("PizzaConnection")));
#endregion

#region Configure Stripe
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
#endregion

#region Configure Serilog
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
#endregion

#region Configure Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole> (options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
    .AddDefaultUI()
    .AddEntityFrameworkStores<PizzaContext>()
    .AddDefaultTokenProviders();
#endregion

#region Configure Authentication (Google)
builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["GoogleConnection:ClientId"]!;
        googleOptions.ClientSecret = builder.Configuration["GoogleConnection:ClientSecret"]!;
        googleOptions.Scope.Add("https://www.googleapis.com/auth/photoslibrary");
    })
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = builder.Configuration["FacebookConnection:AppId"]!;
        facebookOptions.AppSecret = builder.Configuration["FacebookConnection:AppSecret"]!;
    });
#endregion

#region Configure razor pages
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options => {
        options.Conventions.AddPageRoute("/ProductType/Index", "");
    });
#endregion

#region Configure EmailSender
builder.Services.AddTransient<IEmailSender, EmailSender>();
#endregion

#region Configure AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region Configure Own Servises
//Add own services to the container
builder.Services.AddCoreServices();
#endregion


var app = builder.Build();
app.Logger.LogInformation("App created...");

#region Migrations running and Seed
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var context = scopedProvider.GetRequiredService<PizzaContext>();       
        if (context.Database.IsSqlServer())
        {
            app.Logger.LogInformation("Database migration running...");
            context.Database.Migrate();
        }
        var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await PizzaContextSeed.SeedAsync(context, app.Logger, userManager, roleManager);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred adding migrations to Database.");
    }
}
#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization("en-US", "en-US");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Logger.LogDebug("Starting the app...");
app.Run();
