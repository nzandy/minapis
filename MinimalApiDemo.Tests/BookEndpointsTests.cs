using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiDemo.Data;
using MinimalApiDemo.Tests.Helpers;
using Xunit;

namespace MinimalApiDemo.Tests
{
    public class BookEndpointsTests
    {
        [Fact]
        public async Task GetById_ShouldReturn404_WhenBookWithIdDoesntExist()
        {
            // Arrange.
            var webAppFactory = new TestWebApplicationFactory();
            var httpClient = webAppFactory.CreateClient();

            // Act.
            var response = await httpClient.GetAsync($"v1/books/1");

            // Assert.
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Fact]
        public async Task GetById_ShouldReturnBook_WhenIdExists()
        {
            // Arrange.
            var webAppFactory = new TestWebApplicationFactory();
            var services = webAppFactory.Services;

            var book = new Book
            {
                Id = 1,
                Title = "Harry Potter"
            };
            using (var scope = services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<BookContext>();
                dbContext.Books.Add(book);
                await dbContext.SaveChangesAsync();
            }
            var httpClient = webAppFactory.CreateClient();

            
            // Act.
            var response = await httpClient.GetAsync($"v1/books/{book.Id}");

            
            // Assert.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<Book>();

            Assert.Equivalent(book, result);

        }
    }
}
