using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenefitCsBdd
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string MemberId { get; set; }
        public string ProductId { get; set; }
        public int Tier { get; set; }
        public string ClaimDesc { get; set; }
        public decimal Amount { get; set; }
    }
}