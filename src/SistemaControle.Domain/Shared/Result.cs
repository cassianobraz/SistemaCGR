namespace SistemaControle.Domain.Shared;

public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? CodeError { get; }
    public string? Message { get; }
    public T? Value { get; }
    public List<IDomainEvent> DomainEvents { get; } = [];

    protected Result(bool isSuccess, string? codeError, string? message, T? value, List<IDomainEvent>? domainEvents = null)
    {
        IsSuccess = isSuccess;
        CodeError = codeError;
        Message = message;
        Value = value;
        if (domainEvents is not null)
            DomainEvents = domainEvents;
    }

    protected Result(bool isSuccess, string? codeError, string? message, List<IDomainEvent>? domainEvents = null)
    {
        IsSuccess = isSuccess;
        CodeError = codeError;
        Message = message;
        if (domainEvents is not null)
            DomainEvents = domainEvents;
    }

    public static Result<T> Success(T value, List<IDomainEvent>? domainEvents = null) =>
        new(true, null, null, value, domainEvents);

    public static Result<T> Failure(string codeError, string message, List<IDomainEvent>? domainEvents = null) =>
        new(false, codeError, message, default, domainEvents);

    public static Result<T> Success(List<IDomainEvent>? domainEvents = null) =>
        new(true, null, null, domainEvents);
}

