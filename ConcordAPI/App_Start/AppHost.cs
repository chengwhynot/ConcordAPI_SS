using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ConcordAPI.ServiceInterface;
using ConcordAPI.Models;

namespace ConcordAPI
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Concord API by SS", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Funq.Container container)
        {
            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[] { 
                    new ConcordyaBasicAuthProvider() //Sign-in with Basic Auth
                }));

            Plugins.Add(new RegistrationFeature());

            container.Register<ICacheClient>(new MemoryCacheClient());
            //The IUserAuthRepository is used to store the user credentials etc.
            //Implement this interface to adjust it to your app's data storage
            container.Register<IUserAuthRepository>(c =>
                new OrmLiteAuthRepository(container.Resolve<IDbConnectionFactory>()));

            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory());
        }
    }
}