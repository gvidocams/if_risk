using System;

[Serializable]
public class DuplicatePolicyException : Exception
{
    public DuplicatePolicyException()
        : base("Can't sell an already existing policy!") { }
}
