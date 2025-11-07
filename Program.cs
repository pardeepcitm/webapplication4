using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;

var builder = WebApplication.CreateBuilder(args);


// ✅ Add Key Vault (works locally and in Azure)
builder.Configuration.AddAzureKeyVault(
    new Uri("https://myfirstkeyvault0508.vault.azure.net/"),
    new DefaultAzureCredential()
);

// ✅ Read secret from Key Vault  lTq8Q~.2Q9U6_KYr9Vxn3FYOppc8284CFOLEoa.K
string connectionString = string.Empty;
try
{
    connectionString = builder.Configuration["PostgresConnectionString"];
}
catch (Exception ex)
{
    Console.WriteLine("KeyVault Error: " + ex.Message);
}


// ✅ Add PostgreSQL DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// ✅ Add Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger"; // you can change URL
});

app.MapControllers();
app.Run();