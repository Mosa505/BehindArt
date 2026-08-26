using BehindArt.Application.Interfaces;
using BehindArt.Application.Services;
using BehindArt.Domain.Entitiyes;
using BehindArt.Domain.Interfaces;
using BehindArt.Infrastructure.Data;
using BehindArt.Infrastructure.Repositories;
using BehindArt.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, Role>(options =>
 {
     options.Password.RequiredLength = 6;
     options.Password.RequireNonAlphanumeric = false;
     options.Password.RequireUppercase = false;
     options.User.RequireUniqueEmail = true;

 }
   ).AddEntityFrameworkStores<AppDbContext>()
   .AddDefaultTokenProviders();

builder.Services.AddScoped<IPaintingRepository, PaintingRepository>();
builder.Services.AddScoped<IPaintingService, PaintingService>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IEraRepository, EraRepository>();
builder.Services.AddScoped<IEraService, EraService>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();

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
