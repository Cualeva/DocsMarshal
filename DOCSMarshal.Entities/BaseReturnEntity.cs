using System;
namespace DocsMarshal.Entities
{
    public class BaseReturnEntity
    {
        public bool HasError { get; set; }
        public string Error { get; set; }

        public BaseReturnEntity()        {            HasError = false;            Error = string.Empty;        }
    }
}
