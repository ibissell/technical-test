using Bissell.Core.Models;
using Bissell.Database;
using Bissell.Database.Entities;
using Bissell.Services.Interfaces;
using Bissell.Services.Repository;
using Bissell.Services.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BugTrackerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BugTracker")));
builder.Services.AddTransient<IRepository<Bug,BugSearchParameters>, BugRepository>();
builder.Services.AddTransient<IRepository<Person,PersonSearchParameters>, PersonRepository>();
builder.Services.AddTransient<IRepository<BugHistory, BugSearchParameters>, BugHistoryRepository>();
builder.Services.AddTransient<IBugService, BugService>();
builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
