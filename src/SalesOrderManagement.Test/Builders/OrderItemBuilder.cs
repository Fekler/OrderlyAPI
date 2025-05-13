using SalesOrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderManagement.Test.Builders
{
    public class OrderItemBuilder
    {
        
        private int _id = 2;
        private Guid _uuid = Guid.CreateVersion7(); 
        private DateTime _createAt = DateTime.UtcNow;
        private DateTime? _updateAt = null;

        private Guid _orderId = Guid.CreateVersion7();
        private Order _order = null; 

        private Guid _productId = Guid.CreateVersion7();
        private Product _product = null; 

        private int _quantity = 1;
        private decimal _unitPrice = 10.00m;
        private decimal _totalPrice = 10.00m; 

        public OrderItemBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public OrderItemBuilder WithUuid(Guid uuid)
        {
            _uuid = uuid;
            return this;
        }

        public OrderItemBuilder WithCreateAt(DateTime createAt)
        {
            _createAt = createAt;
            return this;
        }

        public OrderItemBuilder WithUpdateAt(DateTime? updateAt)
        {
            _updateAt = updateAt;
            return this;
        }

        public OrderItemBuilder WithOrderId(Guid orderId)
        {
            _orderId = orderId;
            return this;
        }

        public OrderItemBuilder WithOrder(Order order)
        {
            _order = order;
            _orderId = order.UUID; 
            return this;
        }

        public OrderItemBuilder WithProductId(Guid productId)
        {
            _productId = productId;
            return this;
        }

        public OrderItemBuilder WithProduct(Product product)
        {
            _product = product;
            _productId = product.UUID;
            return this;
        }

        public OrderItemBuilder WithQuantity(int quantity)
        {
            _quantity = quantity;
            if (_unitPrice > 0)
            {
                _totalPrice = _quantity * _unitPrice;
            }
            return this;
        }

        public OrderItemBuilder WithUnitPrice(decimal unitPrice)
        {
            _unitPrice = unitPrice;
            _totalPrice = _quantity * _unitPrice;
            return this;
        }

        public OrderItemBuilder WithTotalPrice(decimal totalPrice)
        {
            _totalPrice = totalPrice;
            return this;
        }

        public OrderItem Build()
        {

            var orderItem = new OrderItem
            {
                Id = _id,
                UUID = _uuid,
                CreateAt = _createAt,
                UpdateAt = _updateAt,
                OrderId = _orderId,
                ProductId = _productId,
                Quantity = _quantity,
                UnitPrice = _unitPrice,
                TotalPrice = _totalPrice
            };

            if (_order != null)
            {
                orderItem.Order = _order;
            }
            if (_product != null)
            {
                orderItem.Product = _product;
            }

            return orderItem;
        }
    }
}

