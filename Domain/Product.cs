namespace Domain
{
    public record Product
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required float Price { get; init; }
        public required float Weight { get; init; }
        public required ProductType ProductType { get; init; }
        public required DateTimeOffset CreationDate { get; init; }
        public required int WarehouseId { get; init; }
    }
}
