using AutoFixture;
using Domain.Services;
using Domain;
using Moq;
using Domain.Exceptions;

namespace UnitTests
{
    public class ProductServiceCreateTests
    {
        private readonly IProductService _productService;
        private Mock<IProductRepository> _productRepositoryMock;

        public ProductServiceCreateTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public void Greate_SuccessfulCreation_ShouldReturnProductId()
        {
            // Arrange
            var productDto = new Fixture().Create<ProductDto>();
            var expectedId = 1;

            _productRepositoryMock
                .Setup(f => f.Create(productDto))
                .Returns(expectedId);

            // Act
            int actualId = _productService.Create(productDto);

            // Assert
            Assert.Equal(actualId, expectedId);
            _productRepositoryMock.Verify(f => f.Create(productDto), Times.Once);
        }

        [Fact]
        public void Greate_СreationFailed_ShouldThrowAbortedException()
        {
            // Arrange
            var productDto = new Fixture().Create<ProductDto>();

            _productRepositoryMock
                .Setup(f => f.Create(productDto))
                .Throws(new AbortedException());

            // Act
            Action createProductAction = () => _productService.Create(productDto);

            // Assert
            Assert.Throws<AbortedException>(createProductAction);
            _productRepositoryMock.Verify(f => f.Create(productDto), Times.Once);
        }
    }
}
