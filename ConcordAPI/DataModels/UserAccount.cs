using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Auth;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;

namespace ConcordAPI.DataModels
{
    public class UserAccount:UserAuth
    {
        public string Password { get; set; }
        public string Pin { get; set; }
        [Alias("Last_Login_Time")]
        public DateTime LastLoginTime { get; set; }
        
        public int CreatedBy { get; set; }
        public DateTime Create_On { get; set; }
        public int Last_Updated_By { get; set; }
        public DateTime Last_Updated_On { get; set;}
        public string Description { get; set; }
        public int Role { get; set; }
    }

    public class UserAccountDetail:UserAuthDetails
    {

    }
}