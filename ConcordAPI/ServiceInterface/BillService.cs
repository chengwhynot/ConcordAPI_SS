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
    //[Route("/bills","GET")]
    public class Bills:QueryBase<Bill>
    {
        public int CreateBy { get; set; }
        public int Id { get; set; }
    }

    public class GetBillsResponse
    {
        public List<Bill> Bills { get; set; }
    }

    [Route("/bill", "POST,GET")]
    public class Bill
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        public string BillNum { get; set; }

        public string Desciption { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? DueDate { get; set; }

        public int Category { get; set; }

        public DateTime? GlPostingDate { get; set; }

        public decimal Amount { get; set; }

        public string Attachment { get; set; }

        public string State { get; set; }

        public string Reciept_Num { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
    }

    
    public class BillService : Service
    {
        //public IAutoQuery AutoQuery { get; set; }
        public IDbConnectionFactory dbFactory { get; set; }
        //private IDbCommand dbCmd { get; set; }
        public BillService()
        {
        }

        public object Post(ServiceInterface.Bill dto)
        {
            using (var dbConn = dbFactory.OpenDbConnection())
            {
                dto.DueDate = (dto.DueDate == null ? DateTime.Now : dto.DueDate);
                dto.InvoiceDate = (dto.InvoiceDate == null ? DateTime.Now : dto.InvoiceDate);
                dto.GlPostingDate = (dto.GlPostingDate == null ? DateTime.Now : dto.GlPostingDate);
                dto.CreatedOn = DateTime.Now;
                dto.LastUpdatedOn= DateTime.Now;
                return dbConn.Insert<Bill>(dto, true);
            }
        }

        public object Update(Bill dto)
        {
            return dto;
        }
        public object Get(Bill dto)
        {
            var b = dbFactory.OpenDbConnection().LoadSingleById<Bill>(dto.Id);
            return b;
        }
    }
}