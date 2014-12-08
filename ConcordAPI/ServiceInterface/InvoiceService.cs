using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcordAPI.DataModels;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace ConcordAPI.ServiceInterface
{
    [Route("/invoice", "POST,GET")]
    public class Invoice : ModelBase
    {
        [Alias("Invoice_Num")]
        public string InvoiceNum { get; set; }
        public Bill Bill { get; set; }
        [Alias("Bill_Date")]
        public DateTime? BillDate { get; set; }
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

    public class InvoiceService : Service
    {
        public IDbConnectionFactory dbFactory { get; set; }

        public object Post(Invoice dto)
        {
            using (var dbConn = dbFactory.OpenDbConnection())
            {
                dto.DueDate = (dto.DueDate == null ? DateTime.Now : dto.DueDate);
                dto.BillDate = (dto.BillDate == null ? DateTime.Now : dto.BillDate);
                dto.GlPostingDate = (dto.GlPostingDate == null ? DateTime.Now : dto.GlPostingDate);
                dto.Create_On = DateTime.Now;
                dto.Last_Updated_On = DateTime.Now;
                return dbConn.Insert<Invoice>(dto,true);
            }
        }

        public object Get(Invoice dto)
        {
            return dbFactory.OpenDbConnection().LoadSingleById<Invoice>(dto.Id);
        }
    }
}