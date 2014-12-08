using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;

namespace ConcordAPI.DataModels
{
    public class ModelBase
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int Created_By { get; set; }
        public DateTime Create_On { get; set; }
        public int Last_Updated_By { get; set; }
        public DateTime Last_Updated_On { get; set; }
        public string Description { get; set; }
    }
}