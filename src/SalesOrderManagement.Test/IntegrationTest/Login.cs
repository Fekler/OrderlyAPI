using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SalesOrderManagement.API; 
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SalesOrderManagement.Application.Dtos.Auth;
using SharedKernel.Utils;

namespace SalesOrderManagement.Test.IntegrationTest;

public class Login(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory< Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact(DisplayName = "Deve realizar login com sucesso para o usuário master")]
    public async Task Login_DeveRetornarToken_QuandoCredenciaisForemValidas()
    {
        // Arrange
        var loginRequest = new
        {
            email = "master@email.com",
            password = "SenhaMaster@123"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<AuthenticationResponse>>();

        result.Should().NotBeNull();
        result.Data.AccessToken.Should().NotBeNullOrWhiteSpace();
    }




}
