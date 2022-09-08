using System;

[Serializable]
public class InvalidRiskException : Exception
{
    public InvalidRiskException()
        : base("This insurance company doesn't insure this risk!") { }
}

