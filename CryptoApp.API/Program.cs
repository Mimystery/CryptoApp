using CryptoApp.API.Extensions;
using CryptoApp.Application.Authentication;
using CryptoApp.Application.Services;
using CryptoApp.DataAccess;
using CryptoApp.DataAccess.Repositories;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowRender", policy =>
    {
        policy.WithOrigins("https://cryptoapp-1-dsnm.onrender.com") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddDbContext<CoinsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CoinsDbContext)));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICoinsRepository, CoinsRepository>();
builder.Services.AddScoped<ICoinsService, CoinsService>();

builder.Services.AddScoped<ITelegramUserRepository, TelegramUserRepository>();
builder.Services.AddScoped<ITelegramUserService, TelegramUserService>();

builder.Services.AddScoped<IJwtService, JwtService>();

var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
builder.Services.AddApiAuthentication(Options.Create(jwtOptions));

builder.Services.AddHttpContextAccessor();


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
    db.Database.Migrate(); 
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowRender");

app.UseHttpsRedirection();

//app.UseCookiePolicy(new CookiePolicyOptions
//{
//    MinimumSameSitePolicy = SameSiteMode.Strict,
//    HttpOnly = HttpOnlyPolicy.Always,
//    Secure = CookieSecurePolicy.Always
//});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
