using System;

namespace DocsMarshal.Connectors.Entities
{
    public class User
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public bool CanConfigure { get; set; }
        public string AccessKey { get; set; }
        public int DomainId { get; set; }
        public int? LanguageId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
        public string Password { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastPswUpdatedDt { get; set; }
        public string MobilePhone { get; set; }
        public bool? PasswordNeverExpire { get; set; }
        public Guid? RequestLostPswToken { get; set; }
        public string SSOUserName { get; set; }
        public string StaticSessionId { get; set; }
        public bool UserMustChangePasswordAtFirstLogon { get; set; }
        public string ImpersonateAllowedUserIds { get; set; }

        public User()
        {

        }
    }
}