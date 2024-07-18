using Domain;
using Domain.Services;
using Moq;
using AutoFixture;
using Domain.Exceptions;

namespace UnitTests
{
    public class ProductServiceGetTests
    {
        private readonly IProductService _productService;
        private Mock<IProductRepository> _productRepositoryMock;

        public ProductServiceGetTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public void Get_ProductExistsInRepository_ShouldReturnProductFromRepository()
        {
            // Arrange
            int productId = 1;
            var expectedProduct = new Fixture().Create<Product>();

            _productRepositoryMock
                .Setup(f => f.Get(productId))
                .Returns(expectedProduct);

            // Act
            var actualProduct = _productService.Get(productId);

            // Assert
            Assert.NotNull(actualProduct);
            Assert.Equal(expectedProduct, actualProduct);
            _productRepositoryMock.Verify(f => f.Get(productId), Times.Once);
        }

        [Fact]
        public void Get_ProductNotExistsInRepository_ShouldThrowNotFoundException()
        {
            // Arrange
            int productId = 1;

            _productRepositoryMock
                .Setup(f => f.Get(productId))
                .Throws(new NotFoundException());

            // Act
            Action getProductAction = () => _productService.Get(productId);

            // Assert
            Assert.Throws<NotFoundException>(getProductAction);
            _productRepositoryMock.Verify(f => f.Get(productId), Times.Once);
        }
    }
}