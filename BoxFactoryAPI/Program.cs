using Application.DTOs;
using AutoMapper;
using Domain;
using FluentValidation;
using Infrastructure;
using Infrastructure.DependencyResolver;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Starting API");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

var mapper = new MapperConfiguration(configuration =>
{
    configuration.CreateMap<PostBoxDTO, Box>();
}).CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<BoxDbContext>(options => options.UseSqlite("Data source=db.db"));

Application.DependencyResolver
              .DependencyResolverService
              .RegisterApplicationLayer(builder.Services);

DependencyResolverService.RegisterInfrastructure(builder.Services);

builder.Services.AddCors();

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