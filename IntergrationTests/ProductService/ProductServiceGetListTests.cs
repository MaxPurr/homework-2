using Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace IntergrationTests.ProductService
{
    public class ProductServiceGetListTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _webAppFactory;
        public ProductServiceGetListTests(WebApplicationFactory<Program> webAppFactory)
        {
            _webAppFactory = webAppFactory;
        }

        [Fact]
        public async Task GetList_PageExistsWithoutFilter_ReturnsPageOfProductList()
        {
            // Arrange
            HttpClient client = _webAppFactory.CreateClient();
            await ProductServiceTestHelper.CreateExampleProductListAsync(client);
            int page = 1;
            int count = 5;
            int expectedCount = 5;

            // Act
            var responce = await client.GetAsync($"/v1/product/getlist?page={page}&count={count}");
            var content = await responce.Content.ReadAsStringAsync();
            var getProductListReply = GetProductListReply.Parser.ParseJson(content);

            // Assert
            responce.StatusCode.Should().Be(HttpStatusCode.OK);
            getProductListReply.Products.Count.Should().Be(expectedCount);
        }

        [Fact]
        public async Task GetList_PageNotExistsWithoutFilter_ReturnsEmptyProductList()
        {
            // Arrange
            HttpClient client = _webAppFactory.CreateClient();
            int page = 10;
            int count = 5;
            int expectedCount = 0;

            // Act
            var responce = await client.GetAsync($"/v1/product/getlist?page={page}&count={count}");
            var content = await responce.Content.ReadAsStringAsync();
            var getProductListReply = GetProductListReply.Parser.ParseJson(content);

            // Assert
            responce.StatusCode.Should().Be(HttpStatusCode.OK);
            getProductListReply.Products.Count.Should().Be(expectedCount);
        }

        [Fact]
        public async Task GetList_FilterByWarehouseId_ReturnsFilteredProductList()
        {
            // Arrange
            HttpClient client = _webAppFactory.CreateClient();
            await ProductServiceTestHelper.CreateExampleProductListAsync(client);
            int page = 1;
            int count = 5;
            int warehouseId = 1000;
            int expectedCount = 1;

            // Act
            var responce = await client.GetAsync($"/v1/product/getlist?page={page}&count={count}&filter.warehouseId={warehouseId}");
            var content = await responce.Content.ReadAsStringAsync();
            var getProductListReply = GetProductListReply.Parser.ParseJson(content);

            // Assert
            responce.StatusCode.Should().Be(HttpStatusCode.OK);
            getProductListReply.Products.Count.Should().Be(expectedCount);
        }

        [Fact]
        public async Task GetList_PageValidationFailed_ReturnsBadRequestStatusCode()
        {
            // Arrange
            HttpClient client = _webAppFactory.CreateClient();
            int page = -1;
            int count = 5;

            // Act
            var responce = await client.GetAsync($"/v1/product/getlist?page={page}&count={count}");

            // Assert 
            responce.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetList_FilterValidationFailed_ReturnsBadRequestStatusCode()
        {
            // Arrange
            HttpClient client = _webAppFactory.CreateClient();
            int page = 1;
            int count = 5;
            int warehouseId = -1;

            // Act
            var responce = await client.GetAsync($"/v1/product/getlist?page={page}&count={count}&filter.warehouseId={warehouseId}");

            // Assert 
            responce.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
