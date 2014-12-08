using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.Auth;

namespace ConcordAPI.ServiceInterface
{
    public class ConcordyaBasicAuthProvider: BasicAuthProvider
    {
        public override bool TryAuthenticate(ServiceStack.IServiceBase authService, string userName, string password)
        {
            return userName == password;
            //return base.TryAuthenticate(authService, userName, password);
        }

        public override ServiceStack.Web.IHttpResult OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            return base.OnAuthenticated(authService, session, tokens, authInfo);
        }
    }

    public class ConcordyaTokenAuthProvider: AuthProvider
    {

        public override object Authenticate(IServiceBase authService, IAuthSession session, Authenticate request)
        {
            throw new NotImplementedException();
        }

        public override bool IsAuthorized(IAuthSession session, IAuthTokens tokens, Authenticate request = null)
        {
            throw new NotImplementedException();
        }
    }
}