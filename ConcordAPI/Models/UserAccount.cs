using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Auth;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;

namespace ConcordAPI.Models
{
    public class UserAccount:UserAuth
    {
        public string Password { get; set; }
        public string Pin { get; set; }
        public DateTime Last_Login_Time { get; set; }
        public int Created_By { get; set; }
        public DateTime Create_On { get; set; }
        public int Last_Updated_By { get; set; }
        public DateTime Last_Updated_On { get; set;}
        public string Description { get; set; }
        public int Role { get; set; }
        public int PersonId { get; set; }
    }

    public class UserAccountDetail:UserAuthDetails
    {

    }
}