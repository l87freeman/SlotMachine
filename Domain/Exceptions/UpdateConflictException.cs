using System;

namespace Domain.Exceptions;

public class UpdateConflictException : Exception
{
    public UpdateConflictException(string entityType) : base($"Conflict was detected on '{entityType}' entity update")
    {}
}