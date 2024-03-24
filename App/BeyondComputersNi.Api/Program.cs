using BeyondComputersNi.Dal.Database;
using BeyondComputersNi.Dal.Interfaces;
using BeyondComputersNi.Services.Interfaces;
using BeyondComputersNi.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BcniDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(Program).Assembly, typeof(ComputerService).Assembly);

builder.Services.AddTransient<IDataGenerator, DataGenerator>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IComputerService, ComputerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.SeedData();

app.Run();
