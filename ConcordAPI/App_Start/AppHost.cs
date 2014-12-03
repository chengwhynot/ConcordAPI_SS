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
using ServiceStack.Configuration;

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
            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Concordya_Payee_DB.mdf;Initial Catalog=Concordya_Payee_DB;Integrated Security=True", SqlServerDialect.Provider));

            //The IUserAuthRepository is used to store the user credentials etc.
            //Implement this interface to adjust it to your app's data storage
            var dbAuthRepo = new OrmLiteAuthRepository<UserAccount, UserAccountDetail>(container.Resolve<IDbConnectionFactory>());
            var userRepository = dbAuthRepo;
            container.Register<IUserAuthRepository>(userRepository);
            string hash, salt;
            new SaltedHash().GetHashAndSaltString("password1", out hash, out salt);

            userRepository.DropAndReCreateTables();
            userRepository.CreateUserAuth(new UserAccount
            {
                UserName = "chengzh",
                Password = hash,
                FullName = "Cheng Zhang",
                Email = "zhangcheng@concordya.com",
                Salt = salt,
                Roles = new List<string> { RoleNames.Admin },
                Permissions = new List<string> { "Get" },
                CreatedDate = DateTime.Now,
                Create_On = DateTime.Now,
                Last_Login_Time = DateTime.Now,
                Last_Updated_By = 0,
                Last_Updated_On = DateTime.Now
            }, "password1");
        }
    }
}