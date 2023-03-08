using Api.Extensions;
using Api.Extensions.Identity;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServices(builder);

LoggerExtension.Configuration(builder.Logging, builder);

var app = builder.Build();

if(builder.Environment.IsDevelopment())
    app.UseSwaggerConfiguration();

IdentityExtension.SeedIdentityConfiguration(app);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseRateLimiter();

app.Run();
