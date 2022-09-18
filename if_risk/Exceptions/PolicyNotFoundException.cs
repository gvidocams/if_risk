using System;

[Serializable]
public class PolicyNotFoundException : Exception
{
    public PolicyNotFoundException()
        : base("Policy with this insured object doesn't exist!") { }
}

