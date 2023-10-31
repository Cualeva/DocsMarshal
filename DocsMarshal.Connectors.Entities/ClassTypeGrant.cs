namespace DocsMarshal.Connectors.Entities
{
    public class ClassTypeGrant
    {
        public int ClassTypeId { get; set; }
        public int SecurityIdentityId { get; set; }
        public int DomainId { get; set; }
        public int Id { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanShare { get; set; }
        public bool CanRead { get; set; }
        public bool CanInsert { get; set; }
        public bool CanDelete { get; set; }

        public ClassTypeGrant()
        {

        }
    }
}