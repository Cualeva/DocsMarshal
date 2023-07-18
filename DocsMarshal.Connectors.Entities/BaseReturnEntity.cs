using System;
namespace DocsMarshal.Connectors.Entities
{
    public class BaseReturnEntity
    {
        public bool HasError { get; set; }
        public string Error { get; set; }

        public BaseReturnEntity()        {            HasError = false;            Error = string.Empty;        }
    }
}
