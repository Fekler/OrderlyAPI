namespace SalesOrderManagement.Domain.Entities._bases
{
    public class Enums
    {
        public enum UserRole
        {
            Client,
            Seller,
            Admin
        }
        public enum OrderStatus
        {
            Pending,
            Approved,
            Cancelled,
            Processing,
            Shipped,
            Delivered,

        }
        public enum PaymentMethod
        {
            CreditCard,
            DebitCard,
            Pix,
            Cash
        }

    }
}
