using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ConcordAPI.DataModels;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace ConcordAPI.ServiceInterface
{
    [Route("/company", "POST, GET")]
    public class Company : IReturn<CompanyResponse>
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string LogoUrl { get; set; }
        public string Website { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        /// <summary>
        /// one company can have only one address for now
        /// </summary>
        [Ignore]
        public Address Address { get; set; }
    }
    public class CompanyResponse
    {

    }
    public class CompanyService : Service
    {
        public IDbConnectionFactory DbFactory { get; set; }

        public object Get(Company dto)
        {
            var branching = Db.Single(
                Db.From<AddressBranching>()
                .Where(b => b.BranchedCategory == (int)AddressBranching.BranchingTypes.Company && b.BranchedId == dto.Id)
                );
            branching = branching == null ? new AddressBranching() { BranchedId = 0 } : branching;

            var address = Db.SingleById<Address>(branching.BranchedId);
            address = address == null ? new Address() { Id = 0 } : address;

            var company = Db.SingleById<Company>(dto.Id);
            if (company != null)
            {
                company.Address = address;
            }
            return company;
        }

        public object Post(Company company)
        {
            var now = DateTime.UtcNow;
            company.CreatedOn = now;
            company.LastUpdatedOn = now;

            // Address needs to insert into Address table
            var address = company.Address == null ? new Address() { CreatedOn = now, LastUpdatedOn = now } : company.Address;
            var branching = Db.Single(Db.From<AddressBranching>()
                .Where(b => b.BranchedCategory == (int)AddressBranching.BranchingTypes.Company && b.BranchedId == company.Id));

            var existingAddress = Db.SingleById<Address>(branching.BranchedId);

            // Insert into AddressBraching table.
            branching = new AddressBranching()
            {
                BranchedCategory = (int)AddressBranching.BranchingTypes.Company,
                CreatedOn = now,
                LastUpdatedOn = now
            };
            //Direct access to System.Data.Transactions:
            using (IDbTransaction trans = Db.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                Db.Save(company);
                //company.Id populated on Save().
                var companyId = company.Id;
                if (company.Address != null)
                {
                    Db.Save(address);

                    var addressId = address.Id;
                    branching.BranchedId = companyId;
                    branching.AddressId = addressId;
                    Db.Save(branching);
                }
                trans.Commit();
            }
            return company.Id;
        }
    }
}