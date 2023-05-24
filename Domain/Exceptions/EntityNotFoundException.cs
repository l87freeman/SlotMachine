using System;

namespace Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string type, string id) : base($"Entity of type {type} with id {id} not found")
    {}
}