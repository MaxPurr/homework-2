using Api;
using System.Net.Http.Json;
using static Google.Rpc.Context.AttributeContext.Types;

namespace IntergrationTests
{
    public static class ProductServiceTestHelper
    {
        public static async Task<int> CreateExampleProductAsync(HttpClient client)
        {
            var createProductRequest = new CreateProductRequest()
            {
                Name = "Product1",
                Price = 99.99F,
                Weight = 100,
                ProductType = ProductType.Common,
                WarehouseId = 1000,
            };
            var responce = await client.PostAsJsonAsync("/v1/product/create", createProductRequest);
            var content = await responce.Content.ReadAsStringAsync();
            var createProductReply = CreateProductReply.Parser.ParseJson(content);
            return createProductReply.Id;
        }

        public static async Task CreateExampleProductListAsync(HttpClient client)
        {
            List<CreateProductRequest> createProductRequests = new List<CreateProductRequest>()
            {
                new CreateProductRequest()
                {
                    Name = "Product1",
                    Price = 99,
                    Weight = 200,
                    ProductType = ProductType.Common,
                    WarehouseId = 1000,
                },
                new CreateProductRequest()
                {
                    Name = "Product2",
                    Price = 159,
                    Weight = 400,
                    ProductType = ProductType.Food,
                    WarehouseId = 2000,
                },
                new CreateProductRequest()
                {
                    Name = "Product3",
                    Price = 80,
                    Weight = 100,
                    ProductType = ProductType.Technological,
                    WarehouseId = 4000,
                },
                new CreateProductRequest()
                {
                    Name = "Product4",
                    Price = 120,
                    Weight = 300,
                    ProductType = ProductType.Food,
                    WarehouseId = 2000,
                },
                new CreateProductRequest()
                {
                    Name = "Product5",
                    Price = 120,
                    Weight = 300,
                    ProductType = ProductType.HouseholdChemicals,
                    WarehouseId = 2000,
                },
            };

            foreach (var request in createProductRequests)
            {
                await client.PostAsJsonAsync("/v1/product/create", request);
            }
        }
    }
}
