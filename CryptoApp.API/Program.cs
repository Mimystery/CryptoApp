using CryptoApp.Application.Services;
using CryptoApp.DataAccess;
using CryptoApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000"; // Render передаёт PORT

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddDbContext<CoinsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CoinsDbContext)));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICoinsRepository, CoinsRepository>();
builder.Services.AddScoped<ICoinsService, CoinsService>();

builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
