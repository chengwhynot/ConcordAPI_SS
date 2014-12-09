using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcordAPI.DataModels
{
    public class AddressBranching
    {
        public enum BranchingTypes
        {
            Person,
            Company,
            Vendor,
            Customer
        }
        public int Id { get; set; }
        public int AddressId { get; set; }
        public int BranchedId { get; set; }
        public int BranchedCategory { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
    }
}