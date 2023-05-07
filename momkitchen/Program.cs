using Microsoft.EntityFrameworkCore;
using momkitchen.Models;
using momkitchen.Services;
using System.Text.Json.Serialization;
using System.Text.Json;
using momkitchen.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MomkitchenContext>(option => option.UseSqlServer
(builder.Configuration.GetConnectionString("MomkitchenAzure")));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
} ));
builder.Services.AddControllers().AddJsonOptions(x =>
                         x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling =
                                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
