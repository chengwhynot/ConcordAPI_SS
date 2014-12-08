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

    [Route("/bill", "POST,PUT,GET,DELETE")]
    public class Bill
    {
        public int Id { get; set; }
        [Alias("Bill_Num")]
        public string BillNum { get; set; }
        public Invoice Invoice { get; set; }
        [Alias("Invoice_Date")]
        public DateTime? InvoiceDate { get; set; }
        [Alias("Due_Date")]
        public DateTime? DueDate { get; set; }
        public int Category { get; set; }
        [Alias("Gl_Posting_Date")]
        public DateTime? GlPostingDate { get; set; }
        public decimal Amount { get; set; }
        public string Attachment { get; set; }
        public string State { get; set; }
        public string Reciept_Num { get; set; }
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
            return dbFactory.OpenDbConnection().Insert<Bill>(dto, true);
        }

        public object Update(Bill dto)
        {
            return dto;
        }
        public object Get(Bill dto)
        {
            var b = dbFactory.OpenDbConnection().LoadSingleById<DataModels.Bill>(dto.Id);
            return b;
        }
    }
}