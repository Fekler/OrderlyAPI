using SalesOrderManagement.Domain.Validations;
using SalesOrderManagement.Domain.Errors;

namespace SalesOrderManagement.Domain.Entities._bases
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public Guid UUID { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        protected EntityBase()
        {

        }

        protected EntityBase(Guid? uuid = null, DateTime? createAt = null, int? id = null)
        {
            InitializeEntity(uuid, createAt, id);
        }

        protected virtual void InitializeEntity(Guid? uuid, DateTime? createAt, int? id)
        {
            if (!id.HasValue)
            {
                CreateAt = DateTime.UtcNow;
                UUID = Guid.CreateVersion7();
            }
            else
            {
                Id = id.Value;
                UpdateAt = DateTime.UtcNow;
                CreateAt = createAt.Value;
                UUID = uuid.Value;
            }
        }

        public virtual void Validate()
        {
            //InitializeEntity(this.UUID, this.CreateAt, this.Id);

            RuleValidator.Build()
                .When(Id < 0, Error.INVALID_ID)
                .When(UUID == Guid.Empty, Error.INVALID_UUID)
                .When(Id > 0 && CreateAt == default, Error.INVALID_DATE)
                .ThrowExceptionIfExists();
        }
    }
}