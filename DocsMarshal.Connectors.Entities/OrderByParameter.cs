using System;
namespace DocsMarshal.Connectors.Entities
{
    public class OrderByParameter
    {
        public string FieldExternalId { get; set; }
        public string Direction { get; set; }
        public OrderByParameter()
        {
        }
    }
}
