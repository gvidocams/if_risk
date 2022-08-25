using System;

[Serializable]
public class DuplicatePolicyException : Exception
{
    public DuplicatePolicyException(string message) { }
}
