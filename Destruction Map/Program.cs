using Destruction_Map.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Destruction_Map.Models;
using Destruction_Map.Models.Middleware;
using DestructionMapDAL;
using DestructionMapDAL.Entities;
using DestructionMapModel.Abstraction.IMappers;
using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Implementation.Mappers;
using DestructionMapModel.Implementation.Services;
using DestructionMapModel.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Manager = DestructionMapModel.Models.Manager;
using User = Destruction_Map.Models.User; //DestructionMapModel.Models.User;
using Destruction_Map.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var authConnectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ??
                       throw new InvalidOperationException("Connection string 'AuthDbConnection' not found.");

builder.Services.AddDbContext<DestructionMapContext>(options =>
    options
        .UseLazyLoadingProxies()
        .UseSqlServer(connectionString));

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(authConnectionString));
    
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
   .AddRoles<IdentityRole>()
   .AddEntityFrameworkStores<AuthDbContext>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();


builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerOnly", policy => policy.RequireRole("Manager"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

/*
builder.Services.ConfigureApplicationCookie(options =>
{
    //options.AccessDeniedPath = "";
});
*/

builder.Services.AddTransient<DestructionMapContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IMapper<SourceEntity, Source>, SourceMapper>();
builder.Services.AddTransient<IMapper<UserEntity, DestructionMapModel.Models.User>, UserMapper>();
builder.Services.AddTransient<IMapper<ManagerEntity, Manager>, ManagerMapper>();
builder.Services.AddTransient<IMapper<EventEntity, Event>, EventMapper>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IManagerService, ManagerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();



app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Map}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();