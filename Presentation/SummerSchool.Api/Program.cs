using Microsoft.EntityFrameworkCore;
using SummerSchool.Api.Middlewares;
using SummerSchool.App.Handler;
using SummerSchool.DataAccess.MsSql.DbContext;
using SummerSchool.DataAccess.MsSql.Repository;
using SummerSchool.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(BookHandler));
builder.Services.AddScoped<IBookRepository, BookRepository>();

#region EntityFramework

// Use SqlServer

builder.Services.AddDbContext<SummerSchoolDbContext>(options =>
    options.UseSqlServer("Data Source=localhost;Initial Catalog=SummerSchool;User id=sa;Password=1234qqqQ;TrustServerCertificate=True"));

// Use InMemory Database

//builder.Services.AddDbContext<SummerSchoolDbContext>(options =>
//                    options.UseInMemoryDatabase(databaseName: "SummerSchoolDbContext"));

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseTraceMiddleware();
app.UsePerformanceMiddleware();
app.UseExceptionHandlerMiddleware();
//app.UseLoggingMiddleware(); Disabled due to performance !!!

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
