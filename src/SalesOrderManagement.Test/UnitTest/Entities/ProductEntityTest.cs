using SalesOrderManagement.Test.Builders;
using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Domain.Validations;

namespace SalesOrderManagement.Test.UnitTest.Entities
{
    public class ProductEntityTests
    {
        //[Fact]
        //public void Product_ValidData_CreatesSuccessfully()
        //{
        //    // Arrange
        //    var productBuilder = new ProductBuilder();

        //    // Act
        //    var product = productBuilder.Build();

        //    // Assert
        //    Assert.NotNull(product);
        //    Assert.NotEqual(Guid.Empty, product.UUID);
        //    Assert.NotEqual(default, product.CreateAt);
        //    Assert.Equal(productBuilder.Name, product.Name);
        //    Assert.Equal(productBuilder.Description, product.Description);
        //    Assert.Equal(productBuilder.Price, product.Price);
        //    Assert.Equal(productBuilder.Quantity, product.Quantity);
        //    Assert.Equal(productBuilder.Category, product.Category);
        //    Assert.True(product.IsActive);
        //}

        [Fact]
        public void Product_NullName_ThrowsDomainException()
        {
            // Arrange
            var productBuilder = new ProductBuilder().WithName(null);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => productBuilder.Build().Validate());
        }

        [Fact]
        public void Product_EmptyName_ThrowsDomainException()
        {
            // Arrange
            var productBuilder = new ProductBuilder().WithName(string.Empty);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => productBuilder.Build().Validate());
        }

        [Fact]
        public void Product_NameExceedsMaxLength_ThrowsDomainException()
        {
            // Arrange
            var longName = new string('A', Const.NAME_MAX_LENGTH + 1);
            var productBuilder = new ProductBuilder().WithName(longName);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => productBuilder.Build().Validate());
        }

        [Fact]
        public void Product_NegativePrice_ThrowsDomainException()
        {
            // Arrange
            var productBuilder = new ProductBuilder().WithPrice(-1m);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => productBuilder.Build().Validate());
        }

        [Fact]
        public void Product_ZeroPrice_DoesNotThrowException()
        {
            // Arrange
            var productBuilder = new ProductBuilder().WithPrice(0m);

            var product = productBuilder.Build();

            Assert.Equal(0m, product.Price);

        }

        [Fact]
        public void Product_NegativeQuantity_ThrowsDomainException()
        {
            // Arrange
            var productBuilder = new ProductBuilder().WithQuantity(-1);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => productBuilder.Build().Validate());
        }

        [Fact]
        public void Product_NullCategory_ThrowsDomainException()
        {
            // Arrange
            var productBuilder = new ProductBuilder().WithCategory(null);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => productBuilder.Build().Validate());
        }

        [Fact]
        public void Product_EmptyCategory_ThrowsDomainException()
        {
            // Arrange
            var productBuilder = new ProductBuilder().WithCategory(string.Empty);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => productBuilder.Build().Validate());
        }

        [Fact]
        public void Product_CategoryExceedsMaxLength_ThrowsDomainException()
        {
            // Arrange
            var longCategory = new string('B', Const.CATEGORY_MAX_LENGTH + 1);
            var productBuilder = new ProductBuilder().WithCategory(longCategory);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => productBuilder.Build().Validate());
        }

        [Fact]
        public void Product_NegativeId_ThrowsDomainException_BaseValidation()
        {
            // Arrange
            var productBuilder = new ProductBuilder().WithId(-1);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => productBuilder.Build().Validate());
        }

        [Fact]
        public void Product_EmptyUUID_ThrowsDomainException_BaseValidation()
        {
            // Arrange
            var productBuilder = new ProductBuilder().WithUuid(Guid.Empty);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => productBuilder.Build().Validate());
        }

        [Fact]
        public void Product_IdGreaterThanZeroAndDefaultCreateAt_ThrowsDomainException_BaseValidation()
        {
            // Arrange
            var productBuilder = new ProductBuilder().WithId(1).WithCreateAt(default);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => productBuilder.Build().Validate());
        }

    }
}