namespace Shared.Contracts.Errors
{
    public class KnownErrors
    {
        public static Error INVALID_OWNER_NAME = new Error("INVALID_OWNER_NAME", "The owner name entered is invalid");
        public static Error INVALID_DOCUMENT = new Error("INVALID_DOCUMENT", "The document entered is invalid");
        public static Error DOCUMENT_ALREADY_REGISTERED = new Error("DOCUMENT_ALREADY_REGISTERED", "The onwer entered document already has a registered account for this bank");
        public static Error INVALID_PHONE_NUMBER = new Error("INVALID_PHONE_NUMBER", "The phone number entered is invalid");
        public static Error INVALID_EMAIL = new Error("INVALID_EMAIL", "The e-mail entered is invalid");
        public static Error INVALID_ACCOUNT_BRANCH = new Error("INVALID_ACCOUNT_BRANCH", "The account branch entered is invalid");
        public static Error INVALID_ACCOUNT_NUMBER = new Error("INVALID_ACCOUNT_NUMBER", "The account number entered is invalid");
        public static Error INVALID_ISPB = new Error("INVALID_ISPB", "The ispb entered is invalid");
        public static Error ACCOUNT_DOES_NOT_EXISTIS = new Error("ACCOUNT_DOES_NOT_EXISTIS", "The account entered does not exists");

        //entries
        public static Error INVALID_ADDRESSING_KEY = new Error("INVALID_ADDRESSING_KEY", "Addressing Key value or type is invalid");
        public static Error ADDRESSING_KEY_ALREADY_EXISTS = new Error("ADDRESSING_KEY_ALREADY_EXISTS", "The addressing key already exists. Consider open a potability");
        public static Error DOES_NOT_HAVE_ADDRESSING_KEYS = new Error("DOES_NOT_HAVE_ADDRESSING_KEYS", "The account does not have registered addressing keys");
        public static Error ADDRESSING_KEY_DOES_NOT_EXISTS = new Error("ADDRESSING_KEY_DOES_NOT_EXISTS", "The addressing key does not exists");
        public static Error CANNOT_REGISTER_CLAIM_FOR_ADDRESSING_KEY_EVP = new Error("CANNOT_REGISTER_CLAIM_FOR_ADDRESSING_KEY_EVP", "Can not register claim for addressing key EVP");
        public static Error ADDRESSING_KEY_ALREADY_HAS_AN_OPEN_CLAIM = new Error("ADDRESSING_KEY_ALREADY_HAS_AN_OPEN_CLAIM", "This addressingk ey already has an open claim");
        public static Error CLAIM_DOES_NOT_EXISTS = new Error("CLAIM_DOES_NOT_EXISTS", "The requested claim does not exist");


    }
}