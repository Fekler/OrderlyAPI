using SalesOrderManagement.Domain.Entities._bases;
using static SalesOrderManagement.Domain.Entities._bases.Enums;
namespace SalesOrderManagement.Domain.Entities
{
    public class User : EntityBase
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Document { get; set; }
        public  UserRole UserRole { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Order> CreateByUser { get; set; } = [];
        public ICollection<Order> ActionedOrders { get; set; } = [];
    }
}
