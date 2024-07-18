namespace Domain
{
    public class ProductFilterBuilder
    {
        private Func<Product, bool>? _filter;
        private ProductFilterBuilder AddCondition(Func<Product, bool> condition)
        {
            if (_filter == null)
            {
                _filter = condition;
            }
            else
            {
                var prevFilter = _filter;
                _filter = (product) => prevFilter(product) && condition(product);
            }
            return this;
        }

        public ProductFilterBuilder FilterFromDate(DateTimeOffset from)
        {
            return AddCondition((product) => product.CreationDate >= from);
        }

        public ProductFilterBuilder FilterToDate(DateTimeOffset to)
        {
            return AddCondition((product) => product.CreationDate <= to);
        }

        public ProductFilterBuilder FilterByProductType(ProductType productType)
        {
            return AddCondition((product) => product.ProductType == productType);
        }

        public ProductFilterBuilder FilterByWarehouseId(long warehouseId)
        {
            return AddCondition((product) => product.WarehouseId == warehouseId);
        }

        public Func<Product, bool>? GetFilter()
        {
            return _filter;
        }
    }
}
