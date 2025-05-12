using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.Application.Dtos.Entities.User
{
    public class UserDto
    {
        public Guid UUID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Document { get; set; }
        public UserRole UserRole { get; set; }
        public bool IsActive { get; set; }
    }
}
