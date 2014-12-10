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
using ServiceStack.Configuration;
using ConcordAPI.ServiceInterface;
using ConcordAPI.DataModels;

namespace ConcordAPI
{
    public class AppHost : AppHostBase
    {
        private const string db_conn_string = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Concordya_Payee_DB.mdf;Initial Catalog=Concordya_Payee_DB;Integrated Security=True";

        public AppHost()
            : base("Concord API by SS", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Funq.Container container)
        {
            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[] { 
                    new ConcordAPI.ServiceInterface.ConcordyaBasicAuthProvider() //Sign-in with Basic Auth
                }));

            Plugins.Add(new RegistrationFeature());
            //Plugins.Add(new AutoQueryFeature { MaxLimit = 100 });

            container.Register<ICacheClient>(new MemoryCacheClient());

            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(
                    new AppSettings().Get(
                    "ConcordAPI.Properties.Settings.LocalSQLConnectionString", 
                    db_conn_string), SqlServerDialect.Provider));
            
            //container.Resolve
            var userRepository = new OrmLiteAuthRepository<UserAccount, UserAccountDetail>(container.Resolve<IDbConnectionFactory>());
            container.Register<IUserAuthRepository>(userRepository);
            InitialDbTables(container, userRepository);
        }

        private static void InitialDbTables(Funq.Container container, OrmLiteAuthRepository<UserAccount, UserAccountDetail> userRepository)
        {
            string hash, salt;
            new SaltedHash().GetHashAndSaltString("password1", out hash, out salt);

            userRepository.DropAndReCreateTables();
            using (var dbConnection = container.Resolve<IDbConnectionFactory>().OpenDbConnection())
            {
                dbConnection.CreateTable<Bill>(true);
                dbConnection.CreateTable<Invoice>(true);
                dbConnection.CreateTable<AddressBranching>(true);
                dbConnection.CreateTable<Address>(true);
                dbConnection.CreateTable<Company>(true);
                dbConnection.CreateTable<Category>(true);
            }
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
                LastLoginTime = DateTime.Now,
                Last_Updated_By = 0,
                Last_Updated_On = DateTime.Now
            }, "password1");
        }
    }
}
