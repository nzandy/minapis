using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MinimalApiDemo.Tests
{
    public class BookEndpointsTests
    {
        [Fact]
        public void GetById_ShouldReturn404_WhenBookWithIdDoesntExist()
        {
            // Arrange.
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateClient();

            // Act.
            var response = httpClient.GetAsync($"v1/books/3");

            // Assert.
            Assert.Equal(HttpStatusCode.NotFound, response.Result.StatusCode);

        }
    }
}
