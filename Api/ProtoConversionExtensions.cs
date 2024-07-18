using Domain;
using Timestamp = Google.Protobuf.WellKnownTypes.Timestamp;
using ProductProto = Api.Product;
using ProductTypeProto = Api.ProductType;
using ProductDomain = Domain.Product;
using ProductTypeDomain = Domain.ProductType;

namespace Api
{
    public static class ProtoConversionExtensions
    {
        public static Timestamp ToTimestamp(this DateTimeOffset dateTime)
        {
            return Timestamp.FromDateTimeOffset(dateTime);
        }

        public static ProductDto ToProductDto(this CreateProductRequest request)
        {
            var productDto = new ProductDto(request.Name, request.Price, request.Weight, request.ProductType.ToDomain(), request.WarehouseId);
            return productDto;
        }

        public static ProductProto ToProto(this ProductDomain product)
        {
            return new ProductProto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Weight = product.Weight,
                ProductType = product.ProductType.ToProto(),
                CreationDate = product.CreationDate.ToTimestamp(),
                WarehouseId = product.WarehouseId,
            };
        }

        private static TEnum ParseToAnother<TEnum>(this Enum enumeration) where TEnum : struct
        {
            if (Enum.TryParse(enumeration.ToString(), out TEnum result))
            {
                return result;
            }
            throw new NotSupportedException($"Cannot parse value = {enumeration} from type {enumeration.GetType()} to type {typeof(TEnum)}");
        }

        public static ProductTypeDomain ToDomain(this ProductTypeProto productType)
        {
            return productType.ParseToAnother<ProductTypeDomain>();
        }

        public static ProductTypeProto ToProto(this ProductTypeDomain productType)
        {
            return productType.ParseToAnother<ProductTypeProto>();
        }

        public static Func<ProductDomain, bool>? ToDelegate(this ProductFilter productFilter)
        {
            var filterBuilder = new ProductFilterBuilder();
            if (productFilter.FromDate != null)
            {
                DateTimeOffset fromDate = productFilter.FromDate.ToDateTimeOffset();
                filterBuilder.FilterFromDate(fromDate);
            }
            if (productFilter.ToDate != null)
            {
                DateTimeOffset toDate = productFilter.ToDate.ToDateTimeOffset();
                filterBuilder.FilterToDate(toDate);
            }
            if (productFilter.ProductType != ProductTypeProto.Unspecified)
            {
                ProductTypeDomain productType = productFilter.ProductType.ToDomain();
                filterBuilder.FilterByProductType(productType);
            }
            if (productFilter.WarehouseId is int warehouseId)
            {
                filterBuilder.FilterByWarehouseId(warehouseId);
            }
            return filterBuilder.GetFilter();
        }
    }
}