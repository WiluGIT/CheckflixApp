using CheckflixApp.Domain.Common.Primitives;

namespace CheckflixApp.Domain.Common.Errors;
/// <summary>
/// Contains the domain errors.
/// </summary>
public static class DomainErrors
{
    /// <summary>
    /// Contains the user errors.
    /// </summary>
    public static class User
    {
        public static Error NotFound => new Error("User.NotFound", "The user with the specified identifier was not found.", ErrorType.Validation);

        public static Error InvalidPermissions => new Error(
            "User.InvalidPermissions",
            "The current user does not have the permissions to perform that operation.", ErrorType.Validation);

        public static Error DuplicateEmail => new Error("User.DuplicateEmail", "The specified email is already in use.", ErrorType.Validation);
        
        public static Error DuplicateUserName => new Error("User.DuplicateUserName", "The specified user name is already in use.", ErrorType.Validation);

        public static Error CannotChangePassword => new Error(
            "User.CannotChangePassword",
            "The password cannot be changed to the specified password.", ErrorType.Validation);

        public static Error PasswordValidationError => new Error(
            "User.PasswordValidationError",
            "The password validation error occured.", ErrorType.Validation);
    }

    ///// <summary>
    ///// Contains the UserName errors.
    ///// </summary>
    public static class UserName
    {
        public static Error NullOrEmpty => new Error("LastName.NullOrEmpty", "The last name is required.", ErrorType.Validation);

        public static Error LongerThanAllowed => new Error("LastName.LongerThanAllowed", "The last name is longer than allowed.", ErrorType.Validation);
    }

    ///// <summary>
    ///// Contains the email errors.
    ///// </summary>
    public static class Email
    {
        public static Error NullOrEmpty => new Error("Email.NullOrEmpty", "The email is required.", ErrorType.Validation);

        public static Error LongerThanAllowed => new Error("Email.LongerThanAllowed", "The email is longer than allowed.", ErrorType.Validation);

        public static Error InvalidFormat => new Error("Email.InvalidFormat", "The email format is invalid.", ErrorType.Validation);
    }

    ///// <summary>
    ///// Contains the password errors.
    ///// </summary>
    public static class Password
    {
        public static Error NullOrEmpty => new Error("Password.NullOrEmpty", "The password is required.", ErrorType.Validation);

        public static Error TooShort => new Error("Password.TooShort", "The password is too short.", ErrorType.Validation);

        public static Error MissingUppercaseLetter => new Error(
            "Password.MissingUppercaseLetter",
            "The password requires at least one uppercase letter.", ErrorType.Validation);

        public static Error MissingLowercaseLetter => new Error(
            "Password.MissingLowercaseLetter",
            "The password requires at least one lowercase letter.", ErrorType.Validation);

        public static Error MissingDigit => new Error(
            "Password.MissingDigit",
            "The password requires at least one digit.", ErrorType.Validation);

        public static Error MissingNonAlphaNumeric => new Error(
            "Password.MissingNonAlphaNumeric",
            "The password requires at least one non-alphanumeric.", ErrorType.Validation);
    }

    /// <summary>
    /// Contains general errors.
    /// </summary>
    public static class General
    {
        public static Error UnProcessableRequest => new Error(
            "General.UnProcessableRequest",
            "The server could not process the request.", ErrorType.Unexpected);

        public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.", ErrorType.Validation);
    }

    /// <summary>
    /// Contains the authentication errors.
    /// </summary>
    public static class Authentication
    {
        public static Error InvalidEmailOrPassword => new Error(
            "Authentication.InvalidEmailOrPassword",
            "The specified email or password are incorrect.", ErrorType.Validation);
    }
}