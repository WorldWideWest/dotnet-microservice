using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServices(builder);

LoggerExtension.Configuration(builder.Logging, builder);

var app = builder.Build();

if(builder.Environment.IsDevelopment())
    app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseRateLimiter();

app.Run();
