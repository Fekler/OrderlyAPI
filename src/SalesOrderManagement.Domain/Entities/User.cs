using SalesOrderManagement.Domain.Entities._bases;
namespace SalesOrderManagement.Domain.Entities
{
    public class User : EntityBase
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string PhotoUrl { get; set; }
        public string CPF { get; set; }
    }
}
