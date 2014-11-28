using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Funq;
using ServiceStack;

namespace ConcordAPI
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Concord API by SS", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            
        }
    }
}