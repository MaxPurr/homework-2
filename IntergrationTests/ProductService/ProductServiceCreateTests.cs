using Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace IntergrationTests
{
    public class ProductServiceCreateTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _webAppFactory;
        public ProductServiceCreateTests(WebApplicationFactory<Program> webAppFactory)
        {
            _webAppFactory = webAppFactory;
        }

        [Fact]
        public async Task Create_ValidationIsSuccessful_ShouldReturnProductId()
        {
            // Arrange
            var client = _webAppFactory.CreateClient();
            var createProductRequest = new CreateProductRequest()
            {
                Name = "Test",
                Price = 99.99F,
                Weight = 100,
                ProductType = ProductType.Common,
                WarehouseId = 1000,
            };

            // Act
            var createResponce = await client.PostAsJsonAsync("/v1/product/create", createProductRequest);
            var createContent = await createResponce.Content.ReadAsStringAsync();
            var createProductReply = CreateProductReply.Parser.ParseJson(createContent);

            // Assert
            createResponce.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            createProductReply.Should().NotBeNull();

            // Act
            int id = createProductReply.Id;
            var getResponce = await client.GetAsync($"/v1/product/get?id={id}");
            var getContent = await getResponce.Content.ReadAsStringAsync();
            var getProductReply = GetProductReply.Parser.ParseJson(getContent);
            var product = getProductReply.Product;

            // Assert
            getResponce.Should().NotBeNull();
            product.Should().NotBeNull();
            product.Id.Should().Be(id);
        }

        [Fact]
        public async Task Create_ValidationFailed_ShouldReturnBadRequestStatusCode()
        {
            // Arrange
            var client = _webAppFactory.CreateClient();
            var createProductRequest = new CreateProductRequest()
            {
                Name = string.Empty,
                Price = -1,
                Weight = -1,
                ProductType = ProductType.Unspecified,
                WarehouseId = 0,
            };

            // Act
            var response = await client.PostAsJsonAsync("/v1/product/create", createProductRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
