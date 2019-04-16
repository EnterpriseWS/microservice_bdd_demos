using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BenefitCsBdd.Controllers
{
    public class DeductibleController : ApiController
    {
        private readonly IBenefit _benefit;

        public DeductibleController(IBenefit benefit)
        {
            _benefit = benefit;
        }

        public IEnumerable<Deductible> Get(string memberId)
        {
            return _benefit.GetDeductible(memberId);
        }

        // GET: api/Default/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
