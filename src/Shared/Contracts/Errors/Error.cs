namespace Shared.Contracts.Errors
{
    public class Error : IError
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }
    }
}