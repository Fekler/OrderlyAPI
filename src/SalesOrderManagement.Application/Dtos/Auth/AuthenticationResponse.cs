namespace SalesOrderManagement.Application.Dtos.Auth
{
    public class AuthenticationResponse
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
