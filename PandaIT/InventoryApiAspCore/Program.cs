using InventoryApiAspCore.Data;
using InventoryApiAspCore.Interfaces;
using InventoryApiAspCore.Interfaces.Auth;
using InventoryApiAspCore.Interfaces.BrandInterface;
using InventoryApiAspCore.Interfaces.CategoryInterface;
using InventoryApiAspCore.Interfaces.Common;
using InventoryApiAspCore.Interfaces.CustomerInterface;
using InventoryApiAspCore.Interfaces.ExpenseInterface;
using InventoryApiAspCore.Interfaces.ProductInterface;
using InventoryApiAspCore.Interfaces.PurchaseInterface;
using InventoryApiAspCore.Interfaces.SupplierInterface;
using InventoryApiAspCore.Middlewares;
using InventoryApiAspCore.Services.Auth;
using InventoryApiAspCore.Services.BrandServices;
using InventoryApiAspCore.Services.CategoryServices;
using InventoryApiAspCore.Services.Common;
using InventoryApiAspCore.Services.CustomerService;
using InventoryApiAspCore.Services.ExpenseServices;
using InventoryApiAspCore.Services.ProductServices;
using InventoryApiAspCore.Services.PurchaseServices;
using InventoryApiAspCore.Services.SupplierService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add logger it will create a file for each day
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/Logged_Info.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Warning()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Mapping services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Swagger Authentication
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Panda IT", Version = "v1" });

    // Define the security scheme
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authentication",
        Description = "Enter your JWT token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        };
    c.AddSecurityRequirement(securityRequirement);
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Data Context
//Data Context
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//Auth Data Context
builder.Services.AddDbContext<InventoryAuthDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NewAuthConnection"));
});

builder.Services.AddScoped(typeof(ICommonService<>), typeof(CommonService<>));
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICustomerService,CustomerService>();
builder.Services.AddScoped<ISupplierService,SupplierService>();
builder.Services.AddScoped<IExpenseTypeService,ExpenseTypeService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

// Assuming ICommonService is your interface and CommonService is the implementation

//Identity
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("InventoryApiAspCore")
    .AddEntityFrameworkStores<InventoryAuthDataContext>()
    .AddDefaultTokenProviders();

//Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

//Set up JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();
