namespace MoviesWebAPI.Utils
{
    public class APIErrorCodes
    {
        public static readonly string HEADER_MISSING_MESSAGE = "The following header value is missing: ";
        public static readonly string BODY_MISSING_MESSAGE = "The request was unsuccessful due to the following missing value in body: ";
        public static readonly string GET_ALL_USERS_REQUEST_EXCEPTION_MESSAGE = "GET_ALL_USERS request was unsuccessful, with the following error message: ";
        public static readonly string GET_USER_BY_ID_RESPONSE_EXCEPTION_MESSAGE = "GET_USER_BY_ID request was unsuccessful, with the following error message: ";
        public static readonly string GET_USER_BY_ID_NOT_FOUND_MESSAGE = "User with given ID doesn't exist in database.";
        public static readonly string GET_USER_BY_NAME_NOT_FOUND_MESSAGE = "User with given name doesn't exist in database.";
        public static readonly string GET_USER_BY_NAME_RESPONSE_EXCEPTION_MESSAGE = "GET_USER_BY_NAME request was unsuccessful, with the following error message: ";
        public static readonly string ADD_NEW_USER_PASSWORD_EXISTS_MESSAGE = "The password entered is already in use.";
        public static readonly string ADD_NEW_USER_EMAIL_EXISTS_MESSAGE = "The email entered is already in use.";
        public static readonly string ADD_NEW_USER_NULL_MESSAGE = "User registration was unsuccessful. Please try again!";
        public static readonly string ADD_NEW_USER_EXCEPTION_MESSAGE = "ADD_NEW_USER request was unsuccessful, with the following error message: ";
        public static readonly string UPDATE_USER_NULL_MESSAGE = "User data update was unsuccessfull. Please try again!";
        public static readonly string UPDATE_USER_EXCEPTION_MESSAGE = "UPDATE_USER_PROFILE request was unsuccessful, with the following error message: ";
    }
}
