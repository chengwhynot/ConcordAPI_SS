using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace ConcordAPI.ServiceInterface
{
    public class ToDo : QueryBase<Bill>
    {
        public IEnumerable<Bill> Bills { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
    }

    [Route("/todo", "GET")]
    public class GetToDosByDate : IReturn<GetToDoResponse>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetToDoResponse
    {
        public ToDo ToDos { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ToDoService:Service
    {
        public object Get(GetToDosByDate dto)
        {
            var todo = new ToDo() {
                    Bills = new List<Bill>(){
                        new Bill(){Id = 1, Amount = 100, State="New"}, 
                        new Bill(){Id = 2, Amount = 20, State="New"}
                    },
                    Invoices = new List<Invoice>(){
                        new Invoice(){},
                        new Invoice(){},
                        new Invoice(){}
                    }
            };
            return new GetToDoResponse() { ToDos = todo };
        }
    }
}