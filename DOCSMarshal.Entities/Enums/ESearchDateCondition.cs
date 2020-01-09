using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.Entities.Enums
{
    public enum ESearchDateCondition
    {
        None = 0,
        LessOf = 1,
        LessOrEqualOf = 2,        
        Equal = 3,
        GreaterOrEqualOf = 4,
        GreaterOf = 5,
        DifferentOf = 6,
        Between = 7,
        IsNull = 8,
        IsNotNull = 9,
        IsNullOrZero = 10,
        IsNotNullAndNotZero = 11,
        NotIncluded = 12,
        IsBeyondThisYear = 13,
        IsLaterThisYear = 14,
        IsLaterThisMonth = 15,
        IsNextWeek = 16,
        IsTomorrow = 17,
        IsToday = 18,
        IsYesterday = 19,
        IsEarlierThisWeek = 20,
        IsLastWeek = 21,
        IsEarlierThisMonth = 22,
        IsEarlierThisYear = 23,
        IsPriorThisYear = 24,
        IsThisWeek = 25,
        UpToYesterday = 26,
        UpToToday = 27,
        FromToday = 28,
        FromTomorrow = 29,
        IsThisMonth = 30,
        IsThisYear = 31,
        TodayIsAnniversary = 32,
        TomorrowWillBeAnniversary = 33,
    }
}
