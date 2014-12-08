using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcordAPI.ServiceInterface;
using ServiceStack.DataAnnotations;

namespace ConcordAPI.DataModels
{
    public class Bill : ModelBase
    {
        public string Bill_Num { get; set; }
        //public string Invoice { get; set; }
        public Invoice Invoice { get; set; }
        [Alias("Invoice_Date")]
        public DateTime InvoiceDate { get; set; }
        [Alias("Due_Date")]
        public DateTime DueDate { get; set; }
        public int Category { get; set; }
        [Alias("Gl_Posting_Date")]
        public DateTime GlPostingDate { get; set; }
        public decimal Amount { get; set; }
        public string Attachment { get; set; }
        public string State { get; set; }
        public string Reciept_Num { get; set; }
        //[Reference()]
        //public virtual Vendor Vendor { get; set; }
    }
}