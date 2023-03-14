using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyPizza.Infrastructure.Data;
using MyPizza.Infrastructure.Data.Identity;
using MyPizza.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Context 
builder.Services.AddDbContext<PizzaContext>(context =>
        context.UseSqlServer(builder.Configuration.GetConnectionString("PizzaConnection")));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole> (options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
    .AddDefaultUI()
    .AddEntityFrameworkStores<PizzaContext>()
    .AddDefaultTokenProviders();

//Configure Identity
//builder.Services
//    .AddIdentity<ApplicationUser, IdentityRole>()
//    .AddDefaultUI()
//    .AddEntityFrameworkStores<PizzaContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["GoogleConnection:ClientId"]!;
        googleOptions.ClientSecret = builder.Configuration["GoogleConnection:ClientSecret"]!;
    });

// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options => {
        options.Conventions.AddPageRoute("/ProductType/Index", "") ;
    });
//builder.Services.AddControllersWithViews();
builder.Services.AddCoreServices();

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();
app.Logger.LogInformation("App created...");

#region Migrations running
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    //try
    //{
        var context = scopedProvider.GetRequiredService<PizzaContext>();       
        if (context.Database.IsSqlServer())
        {
            app.Logger.LogInformation("Database migration running...");
            context.Database.Migrate();
        }
        var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await PizzaContextSeed.SeedAsync(context, app.Logger, userManager, roleManager);        
    //}
    //catch (Exception ex)
    //{
    //    app.Logger.LogError(ex, "An error occurred adding migrations to Database.");
    //}
}
#endregion

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

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Logger.LogDebug("Starting the app...");
app.Run();
