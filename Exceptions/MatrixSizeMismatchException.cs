using System;

public class MatrixSizeMismatchException : Exception
{
    public MatrixSizeMismatchException(string message) : base(message) { }
}
