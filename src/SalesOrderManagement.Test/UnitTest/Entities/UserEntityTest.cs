using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Domain.Validations;
using SalesOrderManagement.Test.Builders;

namespace SalesOrderManagement.Test.UnitTest.Entities
{
    public class UserEntityTest
    {
        //[Fact]
        //public void User_ValidData_CreatesSuccessfully()
        //{
        //    // Arrange
        //    var userBuilder = new UserBuilder();

        //    // Act
        //    var user = userBuilder.Build();

        //    // Assert
        //    Assert.NotNull(user);
        //    Assert.NotEqual(Guid.Empty, user.UUID);
        //    Assert.NotEqual(default, user.CreateAt);
        //    Assert.Equal(userBuilder.FullName, user.FullName);
        //    Assert.Equal(userBuilder.Email, user.Email);
        //    Assert.Equal(userBuilder.Phone, user.Phone);
        //    Assert.Equal(userBuilder.Password, user.Password);
        //    Assert.Equal(userBuilder.Document, user.Document);
        //    Assert.Equal(userBuilder.UserRole, user.UserRole);
        //    Assert.True(user.IsActive);
        //}

        [Fact]
        public void User_NullFullName_ThrowsDomainException()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithFullName(null);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_EmptyFullName_ThrowsDomainException()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithFullName(string.Empty);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_FullNameExceedsMaxLength_ThrowsDomainException()
        {
            // Arrange
            var longName = new string('A', Const.NAME_MAX_LENGTH + 1);
            var userBuilder = new UserBuilder().WithFullName(longName);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_NullEmail_ThrowsDomainException()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithEmail(null);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_EmptyEmail_ThrowsDomainException()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithEmail(string.Empty);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_InvalidEmailFormat_ThrowsDomainException()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithEmail("invalid-email");

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_NullPhone_ThrowsDomainException()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithPhone(null);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_EmptyPhone_ThrowsDomainException()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithPhone(string.Empty);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_InvalidPhoneFormat_ThrowsDomainException()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithPhone("123"); // Formato inválido

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("weak")]
        [InlineData("Teste1")]
        public void User_Invalid_Password_ThrowsDomainException(string password)
        {
            // Arrange
            var userBuilder = new UserBuilder().WithPassword(password);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }


        


        [Fact]
        public void User_NullDocument_ThrowsDomainException()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithDocument(null);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_EmptyDocument_ThrowsDomainException()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithDocument(string.Empty);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_NegativeId_ThrowsDomainException_BaseValidation()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithId(-1);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_EmptyUUID_ThrowsDomainException_BaseValidation()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithUuid(Guid.Empty);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }

        [Fact]
        public void User_IdGreaterThanZeroAndDefaultCreateAt_ThrowsDomainException_BaseValidation()
        {
            // Arrange
            var userBuilder = new UserBuilder().WithId(1).WithCreateAt(default);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => userBuilder.Build().Validate());
        }


    }
}