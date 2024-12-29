using AirDrop.Application.MVC.IService;
using AirDrop.EndPoint.CustomDocumentFilters;
using AirDrop.EndPoint.Models;
using AirDrop.Infra.Data.Context;
using AirDrop.Infra.IoC;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5001);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
builder.Services.AddLogging();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.DocumentFilter<ExcludeAreaControllersDocumentFilter>();
});

builder.Services.AddScoped<IFileUploadHelper, FileUploadHelper>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirDrop API V1");
    c.RoutePrefix = string.Empty;
});
//app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory()))
});

//mvc
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
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

app.MapControllers();
app.Run();

#region IoC
static void RegisterService(IServiceCollection services)
{
    DependencyContainer.RegisterService(services);
}
#endregion