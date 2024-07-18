using Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace IntergrationTests
{
    public class ProductServiceUpdatePriceTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _webAppFactory;
        public ProductServiceUpdatePriceTests(WebApplicationFactory<Program> webAppFactory)
        {
            _webAppFactory = webAppFactory;
        }

        [Fact]
        public async Task UpdatePrice_ProductExists_ShouldReturnUpdatedProduct()
        {
            // Arrange
            var client = _webAppFactory.CreateClient();
            int id = await ProductServiceTestHelper.CreateExampleProductAsync(client);
            float price = 100;

            // Act
            var updateResponce = await client.PatchAsync($"/v1/product/updateprice?id={id}&price={price}", null);
            var updateContent = await updateResponce.Content.ReadAsStringAsync();
            var updatePriceReply = UpdatePriceReply.Parser.ParseJson(updateContent);

            // Assert
            updateResponce.StatusCode.Should().Be(HttpStatusCode.OK);
            updatePriceReply.Product.Id.Should().Be(id);
            updatePriceReply.Product.Price.Should().Be(price);

            // Act
            var getResponce = await client.GetAsync($"/v1/product/get?id={id}");
            var getContent = await getResponce.Content.ReadAsStringAsync();
            var getProductReply = GetProductReply.Parser.ParseJson(getContent);

            // Assert
            getProductReply.Product.Should().Be(updatePriceReply.Product);
        }

        [Fact]
        public async Task UpdatePrice_ProductNotExists_ShouldReturnNotFoundStatusCode()
        {
            // Arrange
            int id = 100;
            float price = 100;
            var client = _webAppFactory.CreateClient();

            // Act
            var updateResponce = await client.PatchAsync($"/v1/product/updateprice?id={id}&price={price}", null);

            // Assert
            updateResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_PriceValidationFailed_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var client = _webAppFactory.CreateClient();
            int id = await ProductServiceTestHelper.CreateExampleProductAsync(client);
            float price = -1;

            // Act
            var updateResponce = await client.PatchAsync($"/v1/product/updateprice?id={id}&price={price}", null);

            // Assert
            updateResponce.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_IdValidationFailed_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var client = _webAppFactory.CreateClient();
            int id = -1;
            float price = 100;

            // Act
            var updateResponce = await client.PatchAsync($"/v1/product/updateprice?id={id}&price={price}", null);

            // Assert
            updateResponce.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
