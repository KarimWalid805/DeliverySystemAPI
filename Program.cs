using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Read connection string from environment variables first (Render)
// Fall back to appsettings.json if not set
var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "";
var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "";
var user = Environment.GetEnvironmentVariable("DB_USER") ?? "";
var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

string connectionString;

if (!string.IsNullOrEmpty(server) &&
    !string.IsNullOrEmpty(database) &&
    !string.IsNullOrEmpty(user) &&
    !string.IsNullOrEmpty(password))
{
    connectionString = $"Server={server};Database={database};User Id={user};Password={password};TrustServerCertificate=True;";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

// Add DbContext
builder.Services.AddDbContext<onlineDeliveryDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
