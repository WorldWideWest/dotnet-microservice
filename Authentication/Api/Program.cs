using Api.Controllers;
using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();
