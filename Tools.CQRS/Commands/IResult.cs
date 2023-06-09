﻿namespace Tools.CQRS.Commands
{
    public interface IResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        string? Message { get; }
    }
}