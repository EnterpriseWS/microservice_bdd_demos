using System;
using System.Collections.Generic;

namespace BenefitCsBdd
{
    public interface IBenefit
    {
        IEnumerable<Deductible> GetDeductible(string productId);
        IEnumerable<Claim> GetClaim(string memberId);
        OopMax GetOopMax(string memberId);
    }
}