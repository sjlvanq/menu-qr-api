using Microsoft.EntityFrameworkCore;
using MenuApi.Data;
using MenuApi.Models;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<MenuDb>(opt => opt.UseSqlite(builder.Configuration["ConnectionString"]));
builder.Services.AddDbContext<MenuDb>(opt => opt.UseSqlite("Data Source=.\\Data\\SQlLiteDatabase.db"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
