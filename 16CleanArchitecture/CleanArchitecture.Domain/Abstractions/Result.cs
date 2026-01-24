namespace CleanArchitecture.Domain.Abstractions;

public sealed class Result<T>
{
    private Result(T data)
    {
        Data = data;
    }

    private Result(string errorMessage, bool ısSuccessful)
    {
        ErrorMessages = new() { errorMessage };
    }

    private Result(List<string> errorMessages, bool ısSuccessful)
    {
        ErrorMessages = errorMessages;
        IsSuccessful = ısSuccessful;
    }
    public T? Data { get; set; }
    public bool IsSuccessful { get; set; } = true;
    public List<string>? ErrorMessages { get; set; }

    public static Result<T> Success(T data)
    {
        return new Result<T>(data);
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>(errorMessage, false);
    }

    public static Result<T> Failure(List<string> errorMessages)
    {
        return new Result<T>(errorMessages, false);
    }

    public static implicit operator Result<T>(T data)
    {
        return new Result<T>(data);
    }
}