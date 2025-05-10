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
            Processing,
            Shipped,
            Delivered,
            Cancelled,
            Approved
        }
        public enum PaymentMethod
        {
            CreditCard,
            DebitCard,
            BankTransfer,
            Cash,
            Pix
        }

    }
}
