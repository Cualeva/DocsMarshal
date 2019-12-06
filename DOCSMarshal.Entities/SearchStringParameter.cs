﻿using System;
using System.Collections.Generic;

namespace DocsMarshal.Entities
{
    public class SearchStringParameter: SearchParameter
    {
        public new Enums.ESearchStringCondition Condition { get; set; }
        public string Value { get; set; }
        public List<string> Values { get; set; }
        public SearchStringParameter()
        {
        }
    }
}
