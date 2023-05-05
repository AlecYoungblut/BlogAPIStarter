using Crud_Application.Data;
using Crud_Application.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddCors(p => p.AddPolicy("", builder => { 
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); 
}));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Library Service to use it with Dependency Injection in Controllers
builder.Services.AddTransient<IDataService, DataService>();

// Register database
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


app.UseCors("corsapp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
