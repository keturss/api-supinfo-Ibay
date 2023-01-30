using System.Text;
using apiSupinfo.Models.Inputs.Product;
using apiSupinfo.Models.Service;
using apiSupinfo.Models.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;
using ProjetWebAPI.Models.Inputs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();


var mapperConfig = new MapperConfiguration(
    mc =>
    {
        //Input
        mc.CreateMap<UserCreateInput, User>();
        mc.CreateMap<UserLoginInput, User>();
        mc.CreateMap<UserUpdateInput, User>();
        
        mc.CreateMap<ProductCreateInput, Product>();
        mc.CreateMap<ProductUpdateInput, Product>();
        
        mc.CreateMap<CartCreateInput, Cart>();
        mc.CreateMap<CartUpdateInput, Cart>();

    });

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//String connection injection
builder.Services.AddDbContext<DbFactoryContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Production"));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("policyName",
        builder =>
        {
            builder.AllowAnyOrigin()//WithOrigins(policiesConfiguration.Website)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();