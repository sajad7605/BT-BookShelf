using BookStoreApp.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using BookStoreApp.Repositories;
using Microsoft.Extensions.Options;
using BookStoreApp.Migrations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<ConnectionStringModel>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(GenericRepo<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMemoryCache();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.ConfigureApplicationCookie(options =>{
    options.LoginPath="/User/AccessDenied"; // Redirects to login if not authenticated
    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirects to AccessDenied if authenticated but unauthorized
});

var app = builder.Build();
if(args.Length==1 && args[0].ToLower()=="seedroles"){
    SeedData.Seed(app);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

app.UseHttpsRedirection();
app.Run();
