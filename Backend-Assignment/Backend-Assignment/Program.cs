using Backend_Assignment.Controllers;
using Backend_Assignment.DBContext;
using Backend_Assignment.Interfaces;
using Backend_Assignment.Repository;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContextPool<BlogPostContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BlogsDB"), sqlServerOptions => sqlServerOptions.CommandTimeout(60)));
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddScoped<IBlogPost, BlogPostRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
