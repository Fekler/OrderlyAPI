using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SalesOrderManagement.Application.Business;
using SalesOrderManagement.Domain.Entities;
using System.Net;
using SalesOrderManagement.Domain.Interfaces.Repositories;
using SalesOrderManagement.Application.Dtos.Entities.User;
using static SalesOrderManagement.Domain.Entities._bases.Enums;
using SalesOrderManagement.Domain.Validations;
using SalesOrderManagement.Domain.Errors;
using SalesOrderManagement.Test.Builders;


namespace SalesOrderManagement.Test.UnitTest.Business
{
    public class UserBusinessTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILogger<UserBusiness>> _loggerMock;
        private readonly UserBusiness _userBusiness;

        public UserBusinessTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<UserBusiness>>();
            _userBusiness = new UserBusiness(_userRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Add_ShouldReturnConflict_WhenEmailExists()
        {
            var dto = new CreateUserDto { Email = "test@test.com" };
            _userRepositoryMock.Setup(r => r.GetByEmail(dto.Email)).ReturnsAsync(new User());

            var result = await _userBusiness.Add(dto);

            Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
        }


        [Fact]
        public async Task Delete_ByGuid_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            _userRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>())).ReturnsAsync((User?)null);

            var result = await _userBusiness.Delete(Guid.NewGuid());

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Delete_ByGuid_ShouldReturnOk_WhenUserIsDeleted()
        {
            var guid = Guid.CreateVersion7();
            UserBuilder builder = new();
            User user = builder.WithUuid(guid).Build();
            _userRepositoryMock.Setup(r => r.Get(guid)).ReturnsAsync(user);
            _userRepositoryMock.Setup(r => r.Delete(guid)).ReturnsAsync(true);

            var result = await _userBusiness.Delete(guid);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.ApiReponse.Success);
        }

        [Fact]
        public async Task GetDto_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            _userRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>())).ReturnsAsync((User?)null);

            var result = await _userBusiness.GetDto(Guid.NewGuid());

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetDto_ShouldReturnOk_WhenUserExists()
        {
            var guid = Guid.CreateVersion7();
            UserBuilder builder = new();
            User user = builder.WithUuid(guid).Build();
            _userRepositoryMock.Setup(r => r.Get(guid)).ReturnsAsync(user);

            var result = await _userBusiness.GetDto(guid);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.ApiReponse.Success);
        }

        [Fact]
        public async Task GetEntity_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            _userRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>())).ReturnsAsync((User?)null);

            var result = await _userBusiness.GetEntity(Guid.NewGuid());

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetEntity_ShouldReturnOk_WhenUserExists()
        {
            var guid = Guid.CreateVersion7();
            UserBuilder builder = new();
            User user = builder.WithUuid(guid).Build();
            _userRepositoryMock.Setup(r => r.Get(guid)).ReturnsAsync(user);

            var result = await _userBusiness.GetEntity(guid);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.ApiReponse.Success);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            var dto = new UpdateUserDto { UUID = Guid.NewGuid() };
            _userRepositoryMock.Setup(r => r.Get(dto.UUID)).ReturnsAsync((User?)null);

            var result = await _userBusiness.Update(dto);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenUserIsUpdated()
        {
            var guid = Guid.CreateVersion7();

            var dto = new UpdateUserDto { UUID = guid };
            UserBuilder builder = new();
            User user = builder.WithUuid(guid).Build(); user.Validate();
            _userRepositoryMock.Setup(r => r.Get(guid)).ReturnsAsync(user);
            _userRepositoryMock.Setup(r => r.Update(It.IsAny<User>())).ReturnsAsync(true);

            var result = await _userBusiness.Update(dto);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.ApiReponse.Success);
        }

        [Fact]
        public async Task Get_ByEmail_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            _userRepositoryMock.Setup(r => r.GetByEmail(It.IsAny<string>())).ReturnsAsync((User?)null);

            var result = await _userBusiness.Get("notfound@test.com");

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Get_ByEmail_ShouldReturnOk_WhenUserExists()
        {
            UserBuilder builder = new();
            User user = builder.Build();
            _userRepositoryMock.Setup(r => r.GetByEmail(It.IsAny<string>())).ReturnsAsync(user);

            var result = await _userBusiness.Get("found@test.com");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.ApiReponse.Success);
        }

        [Fact]
        public async Task ChangePasswordAsync_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            var dto = new ChangePasswordDto { UUID = Guid.NewGuid(), OldPassword = "old", NewPassword = "new", ConfirmNewPassword = "new" };
            _userRepositoryMock.Setup(r => r.Get(dto.UUID)).ReturnsAsync((User?)null);

            var result = await _userBusiness.ChangePasswordAsync(dto);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            _userRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<User>());

            var result = await _userBusiness.GetAll();

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.ApiReponse.Success);
        }

        [Fact]
        public async Task GetAllByRole_ShouldReturnOk()
        {
            _userRepositoryMock.Setup(r => r.GetAllByRole(It.IsAny<UserRole>())).ReturnsAsync(new List<User>());

            var result = await _userBusiness.GetAllByRole("Admin");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.ApiReponse.Success);
        }
    }

}