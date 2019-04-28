using System;
using System.Collections.Generic;

namespace BenefitCsBdd
{
    public interface IBenefit
    {
        IEnumerable<Deductible> GetDeductibles(string productId);
        OopMax GetOopMax(string productId);
        decimal GetOopMaxMet(string memberId);
    }
}