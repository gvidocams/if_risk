using System;

[Serializable]
public class InvalidPolicyStartDateException : Exception
{
    public InvalidPolicyStartDateException(string message) { }
}

