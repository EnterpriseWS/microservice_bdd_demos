using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BenefitCsBdd.Controllers
{
    public class OopMaxController : ApiController
    {
        private readonly IBenefit _benefit;

        public OopMaxController(IBenefit benefit)
        {
            _benefit = benefit;
        }

        // GET: api/OopMax
        public OopMax Get(string productId)
        {
            return _benefit.GetOopMax(productId);
        }

        public decimal GetMet(string memberId)
        {
            return _benefit.GetOopMaxMet(memberId);
        }
    }
}
