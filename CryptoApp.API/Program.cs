using CryptoApp.Application.Services;
using CryptoApp.DataAccess;
using CryptoApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*");
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

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Host=dpg-d1f9kiemcj7s739j6eo0-a;Port=5432;Database=cryptodb_mrv7;Username=admin;Password=CxLuyrwfzlxTr0TcjYCJEThQRSQWgdmd;SSL Mode=Require;Trust Server Certificate=true


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CoinsDbContext>();
    db.Database.Migrate(); // применяет все миграции
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
