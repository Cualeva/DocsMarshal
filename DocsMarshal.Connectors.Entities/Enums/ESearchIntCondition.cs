using System;
namespace DocsMarshal.Connectors.Entities.Enums
{
    public enum ESearchNumericCondition
    {
        None = 0,        LessOf = 1,        LessOrEqualOf = 2,
        Equal = 3,        GreaterOrEqualOf = 4,        GreaterOf = 5,        DifferentOf = 6,        Between = 7,        IsNull = 8,        IsNotNull = 9,        IsNullOrZero = 10,        IsNotNullAndNotZero = 11,        NotIncluded = 12,        IsEqualToConnectedUserId = 13,        IsEqualToConnectedUserOrGroupsId = 14
    }
}
