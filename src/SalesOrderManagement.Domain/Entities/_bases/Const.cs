﻿namespace SalesOrderManagement.Domain.Entities._bases
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
        public const string SYSTEM_SUCCESS = "System success";
        public const string CREATE_SUCCESS = "Create success";
        public const string UPDATE_SUCCESS = "Update success";
        public const string DELETE_SUCCESS = "Delete success";

        public const string MESSAGE_USER_FOUND = "User found";
        public const string MESSAGE_LOGIN_SUCCESS = "Login successful";
        #endregion
    }
}
