using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcordAPI.DataModels;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace ConcordAPI.ServiceInterface
{
    [Route("/category", "POST, GET")]
    [Route("/category/{Id}","GET")]
    public class Category : ModelBase
    {
        public string Name { get; set; }
    }

    public class CategoryService : Service
    {
        public IDbConnectionFactory DbFactory { get; set; }

        public object Get(Category dto)
        {
            return DbFactory.OpenDbConnection().LoadSingleById<Category>(dto.Id);
        }

        public object Post(Category dto)
        {
            dto.Create_On = DateTime.Now;
            dto.Last_Updated_On = DateTime.Now;
            return DbFactory.OpenDbConnection().Insert<Category>(dto, true);
        }
    }
}