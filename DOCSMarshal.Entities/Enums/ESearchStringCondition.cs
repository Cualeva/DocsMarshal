using System;
namespace DocsMarshal.Entities.Enums
{
    public enum ESearchStringCondition
    {
        None = 0,
        IsEqual = 1,
        IsDifferent = 2,
        StartWith = 3,
        Contains = 4,
        EndWith = 5,
        IsNull = 6,
        IsNotNull = 7,
        IsEmpty = 8,
        IsNotEmpty = 9,
        IsNullOrEmpty = 10,
        IsNotNullAndNotEmpty = 11,
        Like = 12
    }
}
