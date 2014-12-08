using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace ConcordAPI.ServiceInterface
{
    [Route("/company","POST, GET")]
    public class Company : DataModels.ModelBase
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string LogoUrl { get; set; }
        public string Website { get; set; }
    }

    public class CompanyService:Service
    {
        public IDbConnectionFactory DbFactory { get; set; }

        public object Get(Company dto)
        {
            return DbFactory.OpenDbConnection().LoadSingleById<Company>(dto.Id);
        }

        public object Post(Company dto)
        {
            return DbFactory.OpenDbConnection().Insert<Company>(dto,true);
        }
    }
}