using Inkblot.API;
using Inkblot.DataAccess.DataSeeder;
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

var contactEmail = builder.Configuration["ContactEmail"];
app.MapGet("/privacy", () => Results.Content($"""
<!DOCTYPE html>
<html>
<head><title>Crane: Private Journal, Diary - Privacy Policy</title></head>
<body style="font-family: sans-serif; max-width: 600px; margin: 40px auto; padding: 0 20px;">
<h1>Privacy Policy</h1>
<p><strong>Last updated:</strong> February 25, 2026</p>

<h2>What We Collect</h2>
<p>When you sign up for Crane, we collect your email address through Auth0 for authentication purposes. We also store the journal entries you create.</p>

<h2>How We Use Your Data</h2>
<p>Your email is used solely for authentication. Your journal entries are stored securely on our servers so you can access them from your device.</p>

<h2>Data Sharing</h2>
<p>We do not sell, share, or distribute your personal data to third parties.</p>

<h2>Data Security</h2>
<p>All data is encrypted in transit. Your journal entries are associated with your authenticated account and are not accessible to other users.</p>

<h2>Data Deletion</h2>
<p>You can delete individual journal entries within the app. To request full account deletion, contact us at the email below.</p>

<h2>Contact</h2>
<p>If you have questions about this privacy policy, contact us at: <strong>{contactEmail}</strong></p>
</body>
</html>
""", "text/html")).AllowAnonymous();

app.Run();
