using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace ConcordAPI.ServiceInterface
{
    [Route("/verifycode")]
    public class VerifyCode : IReturn<VerifyCodeResponse>
    {
        public string Phone { get; set; }
    }

    public class VerifyCodeResponse
    {
        public bool? VerifyResult { get; set; }
        public ResponseStatus ReponseStatus { get; set; }
    }

    public class VerifyCodeService:Service
    {
        [Authenticate]
        public object Post(VerifyCode verifyRequest)
        {
            var phone = verifyRequest;
            // generate random number and save it to cache for a short period.

            return new VerifyCodeResponse() { VerifyResult = null, ReponseStatus = null};
        }

        public object Get(VerifyCode verifyRequest)
        {
            var phone = base.Request.QueryString["phone"];
            var code = base.Request.QueryString["verifycode"];

            return new VerifyCodeResponse() { VerifyResult = phone==code };
        }

    }
}