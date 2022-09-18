using System;

[Serializable]
public class InvalidDateException : Exception
{
    public InvalidDateException()
        : base("Can't set a date in the past!") { }
}

