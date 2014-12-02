using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcordAPI.Models;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.DataAnnotations;

namespace ConcordAPI.ServiceInterface
{
    [Route("/account", "Get")]
    [Route("/account/register","POST")]
    [Authenticate]
    public class Account : IReturn<AccountResponse>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        public string PrimaryPhone { get; set; }
        public string PrimaryEmail { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public string Token { get; set; }
    }

    public class AccountResponse
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class AccountService : Service
    {
        public object Get(Account account)
        {
            return new AccountResponse() { Id = 1, Account = new Account { UserName = "", PrimaryEmail = "" } };
        }

        public object Post(Account account)
        {
            //TODO: flucient validations
            return new AccountResponse() { Id = 1, Account = new Account { UserName = "", PrimaryEmail = ""} };
        }
    }
}