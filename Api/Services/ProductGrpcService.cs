using Domain;
using Domain.Services;
using Grpc.Core;
using ProductDomain = Domain.Product;
using ProductTypeDomain = Domain.ProductType;

namespace Api.Services
{
    public class ProductGrpcService : ProductService.ProductServiceBase
    {
        private IProductService _productService;
        public ProductGrpcService(IProductService productService)
        {
            _productService = productService;
        }

        public override Task<CreateProductReply> Create(CreateProductRequest request, ServerCallContext context)
        {
            ProductDto productDto = request.ToProductDto();
            int id = _productService.Create(productDto);
            var createProductReply = new CreateProductReply()
            {
                Id = id,
            };
            return Task.FromResult(createProductReply);
        }

        public override Task<GetProductReply> Get(GetProductRequest request, ServerCallContext context)
        {
            ProductDomain product = _productService.Get(request.Id);
            var getProductReply = new GetProductReply()
            {
                Product = product.ToProto(),
            };
            return Task.FromResult(getProductReply);
        }

        public override Task<UpdatePriceReply> UpdatePrice(UpdatePriceRequest request, ServerCallContext context)
        {
            ProductDomain product = _productService.UpdatePrice(request.Id, request.Price);
            var updatePriceReply = new UpdatePriceReply()
            {
                Product = product.ToProto(),
            };
            return Task.FromResult(updatePriceReply);
        }

        public override Task<GetProductListReply> GetList(GetProductListRequest request, ServerCallContext context)
        {
            Func<ProductDomain, bool>? filter = null;
            if (request.Filter != null)
            {
                filter = request.Filter.ToDelegate();
            }
            List<ProductDomain> products = _productService.GetList(request.Page, request.Count, filter);
            var getProductListReply = new GetProductListReply();
            getProductListReply.Products.AddRange(products.Select(p => p.ToProto()));
            return Task.FromResult(getProductListReply);
        }
    }
}
