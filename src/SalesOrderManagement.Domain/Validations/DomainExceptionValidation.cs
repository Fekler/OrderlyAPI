namespace SalesOrderManagement.Domain.Validations
{
    public class DomainExceptionValidation : ArgumentException
    {
        internal string Error { get; set; }

        internal DomainExceptionValidation(string error) : base(error) => Error = error;

    }
}

