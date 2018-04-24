﻿using System;
namespace DocsMarshal.Entities
{
    public class LogonToken
    {
        public LogonToken()
        {
        }
                
        public bool LoggedIn { get; set; }
        public string SessionId { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}