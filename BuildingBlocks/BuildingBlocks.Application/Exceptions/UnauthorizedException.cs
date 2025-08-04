using System;

namespace BuildingBlocks.Application.Exceptions;

public class UnatuthorizedException(string error) : Exception
{
    public string Error { get; } = error;
}