using System;

public class InvalidMatrixOperationException : Exception
{
    public InvalidMatrixOperationException(string message) : base(message) { }
}