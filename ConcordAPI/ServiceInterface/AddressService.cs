using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace ConcordAPI.ServiceInterface
{
    [Route("/address","POST, GET")]
    public class Address:IReturn<AddressResponse>
    {
        public enum AddressTypes
        {
            Home = 1,
            Office,
            Other
        }
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set;}
        public bool IsDefault { get; set; }
        public AddressTypes AddressType { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
    }

    public class AddressResponse
    {

    }

    public class AddressService : Service
    {
        public object Post(Address dto)
        {
            dto.CreatedOn = DateTime.Now;
            dto.LastUpdatedOn = DateTime.Now;
            return Db.Insert<Address>(dto, true);
        }

        public object Get(Address dto)
        {
            return Db.SingleById<Address>(dto.Id);
        }
    }
}