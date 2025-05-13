using SalesOrderManagement.Domain.Entities;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.Test.Builders
{
    public class UserBuilder
    {
        private int _id = 1;
        private Guid _uuid = Guid.CreateVersion7();
        private DateTime _createAt = DateTime.UtcNow;
        private DateTime? _updateAt = DateTime.UtcNow;
        private string _fullName = "UserName";
        private string _email = "user@email.com.br";
        private string _phone = "8499999999";
        private string _password = "Pa$sw0rld";
        private string _document = "11111111111";
        private UserRole _userRole = UserRole.Admin;
        private bool _isActive = true;

        public UserBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public UserBuilder WithUuid(Guid uuid)
        {
            _uuid = uuid;
            return this;
        }

        public UserBuilder WithCreateAt(DateTime createAt)
        {
            _createAt = createAt;
            return this;
        }

        public UserBuilder WithUpdateAt(DateTime? updateAt)
        {
            _updateAt = updateAt;
            return this;
        }

        public UserBuilder WithFullName(string fullName)
        {
            _fullName = fullName;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithPhone(string phone)
        {
            _phone = phone;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public UserBuilder WithDocument(string document)
        {
            _document = document;
            return this;
        }

        public UserBuilder WithUserRole(UserRole userRole)
        {
            _userRole = userRole;
            return this;
        }

        public UserBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        public User Build()
        {
            return new User
            {
                Id = _id,
                UUID = _uuid,
                CreateAt = _createAt,
                UpdateAt = _updateAt,
                FullName = _fullName,
                Email = _email,
                Phone = _phone,
                Password = _password,
                Document = _document,
                UserRole = _userRole,
                IsActive = _isActive
            };
        }
    }
}
