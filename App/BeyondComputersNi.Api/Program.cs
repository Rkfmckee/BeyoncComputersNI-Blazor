using BeyondComputersNi.Dal.Contexts;
using BeyondComputersNi.Dal.Interfaces;
using BeyondComputersNi.Services.Interfaces;
using BeyondComputersNi.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBcniDbContext, BcniDbContext>(_ => new BcniDbContext(builder.Configuration.GetConnectionString("DefaultConnection") ?? ""));
builder.Services.AddScoped<IComputerService, ComputerService>();

builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(Program).Assembly, typeof(ComputerService).Assembly);

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
