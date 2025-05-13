﻿namespace IdFortress.Communication.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }
    public T Value { get; }
    protected Result(bool isSuccess, T value, string error)
    {
        if (isSuccess && error != string.Empty)
            throw new InvalidOperationException();

        if (!isSuccess && value is not null)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new Result<T>(true, value, string.Empty);
    public static Result<T> Failure(string error) => new Result<T>(false, default, error);
}