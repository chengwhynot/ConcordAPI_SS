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
    public class Invoice
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string InvoiceNum { get; set; }
        public string BillNumber { get; set; }
        public DateTime? BillDate { get; set; }
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
                dto.CreatedOn = DateTime.Now;
                dto.LastUpdatedOn = DateTime.Now;
                return dbConn.Insert<Invoice>(dto,true);
            }
        }

        public object Get(Invoice dto)
        {
            return Db.SingleById<Invoice>(dto.Id);
        }
    }
}