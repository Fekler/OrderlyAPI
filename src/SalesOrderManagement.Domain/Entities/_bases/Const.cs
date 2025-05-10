namespace SalesOrderManagement.Domain.Entities._bases
{
    public static class Const
    {
        #region Properties
        public const int NAME_MAX_LENGTH = 255;
        public const int DESCRIPTION_MAX_LENGTH = 1000;
        public const int EMAIL_MAX_LENGTH = 255;
        public const int PHONE_MAX_LENGTH = 20;
        public const int DOCUMENT_MAX_LENGTH = 18;
        public const int CATEGORY_MAX_LENGTH = 100;
        public const int ADDRESS_MAX_LENGTH = 100;
        #endregion



        #region Messages
        public const string MESSAGE_INVALID_LOGIN = "Invalid email or password";
        public const string MESSAGE_UNEXPECTED_ERROR = "An unexpected error occurred";

        public const string MESSAGE_USER_NOT_FOUND = "User not found";
        public const string MESSAGE_USER_ALREADY_EXISTS = "User already exists";
        public const string MESSAGE_USER_NOT_AUTHORIZED = "User not authorized";
        public const string MESSAGE_USER_NOT_ACTIVE = "User not active";

        public const string MESSAGE_LOGIN_SUCCESS = "Login successful";
        #endregion
    }
}
