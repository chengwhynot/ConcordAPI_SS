using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.DataAnnotations;

namespace ConcordAPI.DataModels
{
    public class Person : ModelBase
    {
        public int UserAccountId { get; set; }
    }
}
