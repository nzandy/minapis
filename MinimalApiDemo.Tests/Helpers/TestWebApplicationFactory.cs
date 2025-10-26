using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiDemo.Data;

namespace MinimalApiDemo.Tests.Helpers
{
    internal class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly SqliteConnection _connection;
        public TestWebApplicationFactory()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
                {
                    var descriptor =
                        services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BookContext>));
                    
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    //Setup in-memory sql lite for tests
                    services.AddDbContext<BookContext>(options =>
                    {
                        options.UseSqlite(_connection);
                    });

                    var serviceProvider = services.BuildServiceProvider();
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<BookContext>();
                        db.Database.EnsureCreated();
                    }
                }
            );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
