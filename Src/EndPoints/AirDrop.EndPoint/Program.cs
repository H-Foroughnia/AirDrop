using AirDrop.Application.MVC.IService;
using AirDrop.EndPoint.CustomDocumentFilters;
using AirDrop.EndPoint.Models;
using AirDrop.Infra.Data.Context;
using AirDrop.Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ourAirDrop",
            ValidAudience = "ourAirDrop",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8f71648e-6a50-4cde-8474-bb02a9463b1c"))
        };
    });

RegisterService(builder.Services);
//mvc
builder.Services.AddControllersWithViews();
//mvc

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.DocumentFilter<ExcludeAreaControllersDocumentFilter>();
});

builder.Services.AddScoped<IFileUploadHelper, FileUploadHelper>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//mvc
app.UseStaticFiles();
app.UseRouting();
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
//mvc

app.UseAuthorization();

app.MapControllers();

app.Run();

#region IoC
static void RegisterService(IServiceCollection services)
{
    DependencyContainer.RegisterService(services);
}
#endregion