using AutoFixture;
using Domain;
using Domain.Exceptions;
using Domain.Services;
using Moq;

namespace UnitTests
{
    public class ProductServiceUpdatePriceTests
    {
        private readonly IProductService _productService;
        private Mock<IProductRepository> _productRepositoryMock;

        public ProductServiceUpdatePriceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public void UpdatePrice_ProductExistsInRepository_ShouldReturnUpdatedProduct()
        {
            // Arrange
            int productId = 1;
            float newPrice = 100;
            var oldProduct = new Fixture().Create<Product>();
            var updatedProduct = oldProduct with { Price = newPrice };

            _productRepositoryMock
                .Setup(f => f.Get(productId))
                .Returns(oldProduct);
            _productRepositoryMock
                .Setup(f => f.Update(productId, It.IsAny<ProductDto>()))
                .Returns(updatedProduct);

            // Act
            var actualProduct = _productService.UpdatePrice(productId, newPrice);

            // Assert
            Assert.NotNull(actualProduct);
            Assert.Equal(actualProduct, updatedProduct);
            _productRepositoryMock.Verify(f => f.Get(productId), Times.Once);
            _productRepositoryMock.Verify(f => f.Update(productId, It.IsAny<ProductDto>()), Times.Once);
        }

        [Fact]
        public void UpdatePrice_ProductNotExistsInRepository_ShouldThrowNotFoundException()
        {
            // Arrange
            int productId = 1;
            float newPrice = 100;

            _productRepositoryMock
                .Setup(f => f.Get(productId))
                .Throws(new NotFoundException());

            // Act
            Action updatePriceAction = () => _productService.UpdatePrice(productId, newPrice);

            // Assert
            Assert.Throws<NotFoundException>(updatePriceAction);
            _productRepositoryMock.Verify(f => f.Get(productId), Times.Once);
            _productRepositoryMock.Verify(f => f.Update(productId, It.IsAny<ProductDto>()), Times.Never);
        }

        [Fact]
        public void UpdatePrice_UpdateFailed_ShouldThrowAbortedException()
        {
            // Arrange
            int productId = 1;
            float newPrice = 100;
            var oldProduct = new Fixture().Create<Product>();

            _productRepositoryMock
                .Setup(f => f.Get(productId))
                .Returns(oldProduct);
            _productRepositoryMock
                .Setup(f => f.Update(productId, It.IsAny<ProductDto>()))
                .Throws(new AbortedException());

            // Act
            Action updatePriceAction = () => _productService.UpdatePrice(productId, newPrice);

            // Assert
            Assert.Throws<AbortedException>(updatePriceAction);
            _productRepositoryMock.Verify(f => f.Get(productId), Times.Once);
            _productRepositoryMock.Verify(f => f.Update(productId, It.IsAny<ProductDto>()), Times.Once);
        }
    }
}
