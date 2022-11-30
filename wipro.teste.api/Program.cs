using AutoMapper;
using wipro.teste.api.Controllers.Shared;
using wipro.teste.gateway;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new ItemFilaProfiler()));

builder.Services.AddSingleton(mapperConfig.CreateMapper());
builder.Services.AddScoped<IConectorFila, ConectorFila>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
