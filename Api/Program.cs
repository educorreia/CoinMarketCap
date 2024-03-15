using Aplicacao.Interface;
using Aplicacao.Servico;
using Infraestrutura.DBContext;
using Infraestrutura.Interfaces;
using Infraestrutura.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddTransient<ICryptoCurrencyServico, CryptoCurrencyServico>();

builder.Services.AddScoped<ICryptoCurrencyRepositorio, CryptoCurrencyRepositorio>();
builder.Services.AddScoped<ICryptoCurrencyHistoricalRepositorio, CryptoCurrencyHistoricalRepositorio>();
builder.Services.AddScoped<IWeekCryptoCurrencyRepositorio, WeekCryptoCurrencyRepositorio>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
