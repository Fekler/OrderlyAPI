using SalesOrderManagement.Domain.Entities;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.Test.Builders
{
    public class OrderBuilder
    {
        private int _id = 1;
        private Guid _uuid = Guid.CreateVersion7();
        private DateTime _createAt = DateTime.UtcNow;
        private DateTime? _updateAt = DateTime.UtcNow;
        private DateTime _orderDate = DateTime.UtcNow;
        private decimal TotalAmount = 0.1m;
        private string _shippingAddress = "ShippingAddress";
        private string _billingAddress = "BillingAddress";
        private PaymentMethod PaymentMethod = PaymentMethod.CreditCard;
        private OrderStatus _status = OrderStatus.Pending;
        private Guid _createByUserUuid = Guid.CreateVersion7();
        private Guid? _actionedByUserUuid = Guid.CreateVersion7();
        private DateTime? _actionedAt = DateTime.UtcNow;

        private ICollection<OrderItem> _orderItems { get; set; } = [];

        public OrderBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public OrderBuilder WithUuid(Guid uuid)
        {
            _uuid = uuid;
            return this;
        }

        public OrderBuilder WithCreateAt(DateTime createAt)
        {
            _createAt = createAt;
            return this;
        }

        public OrderBuilder WithUpdateAt(DateTime? updateAt)
        {
            _updateAt = updateAt;
            return this;
        }

        public OrderBuilder WithOrderDate(DateTime orderDate)
        {
            _orderDate = orderDate;
            return this;
        }

        public OrderBuilder WithTotalAmount(decimal totalAmount)
        {
            TotalAmount = totalAmount;
            return this;
        }

        public OrderBuilder WithShippingAddress(string shippingAddress)
        {
            _shippingAddress = shippingAddress;
            return this;
        }

        public OrderBuilder WithBillingAddress(string billingAddress)
        {
            _billingAddress = billingAddress;
            return this;
        }

        public OrderBuilder WithPaymentMethod(PaymentMethod paymentMethod)
        {
            PaymentMethod = paymentMethod;
            return this;
        }

        public OrderBuilder WithStatus(OrderStatus status)
        {
            _status = status;
            return this;
        }

        public OrderBuilder WithCreateByUserUuid(Guid createByUserUuid)
        {
            _createByUserUuid = createByUserUuid;
            return this;
        }

        public OrderBuilder WithActionedByUserUuid(Guid? actionedByUserUuid)
        {
            _actionedByUserUuid = actionedByUserUuid;
            return this;
        }

        public OrderBuilder WithActionedAt(DateTime? actionedAt)
        {
            _actionedAt = actionedAt;
            return this;
        }

        public OrderBuilder WithOrderItems(ICollection<OrderItem> orderItems)
        {
            _orderItems = orderItems;
            return this;
        }

        public OrderBuilder AddOrderItem(OrderItem orderItem)
        {
            _orderItems.Add(orderItem);
            return this;
        }
        public Order Build()
        {
            var order = new Order
            {
                Id = _id,
                UUID = _uuid,
                CreateAt = _createAt,
                UpdateAt = _updateAt,
                OrderDate = _orderDate,
                TotalAmount = TotalAmount,
                ShippingAddress = _shippingAddress,
                BillingAddress = _billingAddress,
                PaymentMethod = PaymentMethod,
                Status = _status,
                CreateByUserUuid = _createByUserUuid,
                ActionedByUserUuid = _actionedByUserUuid,
                ActionedAt = _actionedAt,
                OrderItems = _orderItems
            };
            return order;
        }
    }
}
