using Microsoft.EntityFrameworkCore;
using momkitchen.Models;
using momkitchen.Services;
using System.Text.Json.Serialization;
using momkitchen.Mapper;
using momkitchen.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MomkitchenContext>(option => option.UseSqlServer
(builder.Configuration.GetConnectionString("MomkitchenAzure")));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
} ));
builder.Services.AddControllers().AddJsonOptions(x =>
                         x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling =
                                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.Converters.Add(new DateConverters()));

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyCors");
app.UseAuthorization();

app.MapControllers();

app.Run();
