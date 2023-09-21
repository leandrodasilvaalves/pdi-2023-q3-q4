namespace Shared.Contracts
{
    public class KnownErrors
    {
        public static Error INVALID_OWNER_NAME = new Error("INVALID_OWNER_NAME", "The owner name entered is invalid");
        public static Error INVALID_OWNER_DOCUMENT = new Error("INVALID_OWNER_DOCUMENT", "The onwer document entered is invalid");
        public static Error DOCUMENT_ALREADY_REGISTERED = new Error("DOCUMENT_ALREADY_REGISTERED", "The onwer entered document already has a registered account for this bank");
        public static Error INVALID_PHONE_NUMBER = new Error("INVALID_PHONE_NUMBER", "The phone number entered is invalid");
        public static Error INVALID_EMAIL = new Error("INVALID_EMAIL", "The e-mail entered is invalid");
        public static Error INVALID_ACCOUNT_BRANCH = new Error("INVALID_ACCOUNT_BRANCH", "The account branch entered is invalid");
        public static Error INVALID_ACCOUNT_NUMBER = new Error("INVALID_ACCOUNT_NUMBER", "The account number entered is invalid");
        public static Error INVALID_ISPB = new Error("INVALID_ISPB", "The ispb entered is invalid");
    }
}