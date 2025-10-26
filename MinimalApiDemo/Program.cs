using Microsoft.EntityFrameworkCore;
using MinimalApiDemo.Data;
using MinimalApiDemo.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer(); // Important for minimal APIs!
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    SetupLocalDb();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapBookEndpoints();

app.Run();

return;

void SetupLocalDb()
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetService<BookContext>();
        dbContext.Database.EnsureCreated();
    }
}

public partial class Program
{
}
