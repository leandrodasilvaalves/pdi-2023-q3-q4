namespace Shared.Contracts.Errors
{
    public interface IError
    {
        string Code { get; }
        string Message { get; }
    }
}