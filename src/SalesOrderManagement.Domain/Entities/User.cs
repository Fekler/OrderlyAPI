using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Domain.Errors;
using SalesOrderManagement.Domain.Validations;
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



        public override void Validate()
        {
            //InitializeEntity(UUID, CreateAt, Id);

            base.Validate();

            RuleValidator.Build()
                .When(string.IsNullOrWhiteSpace(FullName) || FullName?.Length > Const.NAME_MAX_LENGTH, Error.INVALID_NAME)
                .When(string.IsNullOrWhiteSpace(Email) || !string.IsNullOrWhiteSpace(Email) && !Validations.Rules.IsValidEmail(Email),Error.INVALID_EMAIL)
                .When(string.IsNullOrWhiteSpace(Phone) || !string.IsNullOrWhiteSpace(Phone) && !Validations.Rules.IsValidPhone(Phone),Error.INVALID_PHONE)
                .When(string.IsNullOrWhiteSpace(Password) || !Validations.Rules.IsValidPassword(Password), Error.INVALID_PASSWORD)
                .When(string.IsNullOrWhiteSpace(Document), Error.INVALID_DOCUMENT)
                .ThrowExceptionIfExists();
        }
    }
}