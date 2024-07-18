using Api;
using FluentAssertions;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntergrationTests
{
    public class ProductServiceGetTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _webAppFactory;
        public ProductServiceGetTests(WebApplicationFactory<Program> webAppFactory)
        {
            _webAppFactory = webAppFactory;
        }

        [Fact]
        public async Task Get_ProductExists_ShouldReturnProduct()
        {
            // Arrange
            var client = _webAppFactory.CreateClient();
            int productId = await ProductServiceTestHelper.CreateExampleProductAsync(client);

            //Act
            var responce = await client.GetAsync($"/v1/product/get?id={productId}");
            var content = await responce.Content.ReadAsStringAsync();
            var getProductReply = GetProductReply.Parser.ParseJson(content);

            //Assert
            responce.StatusCode.Should().Be(HttpStatusCode.OK);
            getProductReply.Should().NotBeNull();
            getProductReply.Product.Id.Should().Be(productId);
        }

        [Fact]
        public async Task Get_ProductNotExists_ShouldReturnNotFoundStatusCode()
        {
            // Arrange
            int productId = 100;
            var client = _webAppFactory.CreateClient();

            // Act
            var responce = await client.GetAsync($"/v1/product/get?id={productId}");

            //Assert
            responce.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_IdValidationFailed_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            int productId = -1;
            var client = _webAppFactory.CreateClient();

            // Act
            var responce = await client.GetAsync($"/v1/product/get?id={productId}");

            //Assert
            responce.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
