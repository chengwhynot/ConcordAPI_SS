using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace ConcordAPI.Models
{
    [Route("/account","POST")]
    public class AccountRequest:IReturn<AccountResponse>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PrimaryPhone{ get; set; }
        public string PrimaryEmail { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Companys { get; set; }
    }

    public class AccountResponse
    {
        public int Id { get; set; }
        public ResponseStatus ResponseStatus{get;set;}
    }
}