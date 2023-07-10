using Serilog;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using WeatherForcast.API.Models;
using WeatherForcast.API.Dto;
using Microsoft.AspNetCore.Identity;
using WeatherForcast.API.Repository.Contracts;
using WeatherForcast.API.Repository;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);
try
{
    Log.Information("Starting web application");
    builder.Host.UseSerilog();

    string connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
    builder.Services.AddDbContext<HotelListingDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });


    // Add services to the container.

    builder.Services.AddAutoMapper(typeof(MapperConfig));

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    builder.Services.AddScoped<ICountriesRepository, CountryRepository>();
    builder.Services.AddScoped<IHotelsRepository, HotelRepository>();

    builder.Services.AddIdentityCore<IdentityUser>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<HotelListingDbContext>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapControllers();

    app.UseCors("AllowAll");

    app.UseSerilogRequestLogging();

    app.UseAuthentication();
    app.UseAuthorization();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly:  " + ex.StackTrace);
}
finally
{
    Log.CloseAndFlush();
}