namespace SalesOrderManagement.Domain.Errors
{
    public static class Error
    {
        public const string INVALID_ID = "Invalid ID.";
        public const string INVALID_UUID = "Invalid UUID.";
        public const string INVALID_NAME = "Invalid Name.";
        public const string INVALID_DESCRIPTION = "Invalid Description.";
        public const string INVALID_DATE = "Invalid Date.";
        public const string INVALID_PRICE = "Invalid Price.";
        public const string INVALID_EMAIL = "Invalid E-mail.";
        public const string INVALID_PHONE = "Invalid Phone.";
        public const string INVALID_DOCUMENT = "Invalid Document.";
        public const string INVALID_CATEGORY = "Invalid Category.";
        public const string INVALID_ADDRESS = "Invalid Address.";
        public const string INVALID_QUANTITY = "Invalid Quantity.";
        public const string INVALID_PASSWORD = "Invalid Password.";


        public const string INVALID_LOGIN = "Invalid Login.";
        public const string INVALID_EMAIL_OR_PASSWORD = "Invalid email or password.";

        public const string INVALID_USER = "Invalid User.";
        public const string USER_NOT_FOUND = "User not found.";
        public const string USER_ALREADY_EXISTS = "User already exists.";
        public const string USER_ALREADY_EXISTS_EMAIL = "User with this email already exists.";
        public const string USER_ALREADY_EXISTS_PHONE = "User with this phone already exists.";
        public const string USER_ALREADY_EXISTS_DOCUMENT = "User with this document already exists.";
        public const string USER_NOT_AUTHORIZED = "User not authorized.";
        public const string USER_NOT_ACTIVE = "User not active.";

        public const string PRODUCT_NOT_FOUND = "Product not found.";
        public const string PRODUCT_ALREADY_EXISTS = "Product already exists.";
        public const string PRODUCT_NOT_ACTIVE = "Product not active.";
        public const string ORDER_NOT_FOUND = "Order not found.";
        public const string ORDER_ITEM_NOT_FOUND = "Order item not found.";
        public const string ORDER_ITEM_ALREADY_EXISTS = "Order item already exists.";
        public const string ORDER_ITEM_NOT_ACTIVE = "Order item not active.";
        public const string ORDER_ALREADY_EXISTS = "Order already exists.";
        public const string ORDER_NOT_ACTIVE = "Order not active.";
        public const string ORDER_ITEM_NOT_BELONG_TO_ORDER = "Order item does not belong to the order.";
        public const string ORDER_ITEM_NOT_BELONG_TO_PRODUCT = "Order item does not belong to the product.";
        public const string ORDER_ITEM_QUANTITY_NOT_AVAILABLE = "Order item quantity not available.";
        public const string ORDER_ITEM_QUANTITY_NOT_VALID = "Order item quantity not valid.";
        public const string ORDER_ITEM_QUANTITY_NOT_VALID_FOR_PRODUCT = "Order item quantity not valid for product.";
        public const string ORDER_ITEM_QUANTITY_NOT_VALID_FOR_ORDER = "Order item quantity not valid for order.";
        public const string ORDER_ITEM_QUANTITY_NOT_VALID_FOR_ORDER_ITEM = "Order item quantity not valid for order item.";


        public const string UNEXPECTED_ERROR = "An unexpected error occurred.";
        public const string UNAUTHORIZED = "Unauthorized.";


    }
}
