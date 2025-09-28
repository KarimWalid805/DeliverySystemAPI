using Microsoft.EntityFrameworkCore; // Ensure this is included


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<onlineDeliveryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Ensure Microsoft.EntityFrameworkCore.SqlServer package is installed

var app = builder.Build();

app.MapControllers();
app.Run();




