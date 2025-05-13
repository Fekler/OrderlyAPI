using SalesOrderManagement.Domain.Entities;

namespace SalesOrderManagement.Test.Builders
{
    public class ProductBuilder
    {
        private int _id = 1;
        private Guid _uuid = Guid.CreateVersion7();
        private DateTime _createAt = DateTime.UtcNow;
        private DateTime? _updateAt = null;
        private string _name = "Test Product";
        private string _description = "A description for the test product.";
        private decimal _price = 19.99m;
        private int _quantity = 100;
        private string _category = "Electronics";
        private bool _isActive = true;

        public ProductBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ProductBuilder WithUuid(Guid uuid)
        {
            _uuid = uuid;
            return this;
        }

        public ProductBuilder WithCreateAt(DateTime createAt)
        {
            _createAt = createAt;
            return this;
        }

        public ProductBuilder WithUpdateAt(DateTime? updateAt)
        {
            _updateAt = updateAt;
            return this;
        }

        public ProductBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ProductBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ProductBuilder WithPrice(decimal price)
        {
            _price = price;
            return this;
        }

        public ProductBuilder WithQuantity(int quantity)
        {
            _quantity = quantity;
            return this;
        }

        public ProductBuilder WithCategory(string category)
        {
            _category = category;
            return this;
        }

        public ProductBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        public Product Build()
        {
            return new Product
            {
                Id = _id,
                UUID = _uuid,
                CreateAt = _createAt,
                UpdateAt = _updateAt,
                Name = _name,
                Description = _description,
                Price = _price,
                Quantity = _quantity,
                Category = _category,
                IsActive = _isActive
            };
        }
    }
}