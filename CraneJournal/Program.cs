using CraneJournal.API;
using CraneJournal.DataAccess.DataSeeder;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Auth0:Domain"];
        options.Audience = builder.Configuration["Auth0:Audience"];
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    // Seed additional development data
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    seeder.SeedDevelopmentData().Wait();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();
app.MapGet("/privacy", () =>
{
    var html = File.ReadAllText(Path.Combine("wwwroot", "privacy.html"));
    var contactEmail = builder.Configuration["ContactEmail"];
    html = html.Replace("{{ContactEmail}}", contactEmail);
    return Results.Content(html, "text/html");
}).AllowAnonymous();

app.Run();
