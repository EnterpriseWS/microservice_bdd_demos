using System;
using System.Collections.Generic;
using System.Linq;

namespace BenefitCsBdd
{
    public class MultiTierBenefit : IBenefit
    {
        private readonly IBenefitRepository _repository;

        public MultiTierBenefit(IBenefitRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Deductible> GetDeductible(string productId)
        {
            return ValidateProductId(productId) ? _repository.GetDeductible(productId) : null;
        }

        public OopMax GetOopMax(string memberId)
        {
            return ValidateProductId(productId) ? _repository.GetDeductible(productId) : null;
        }

        public IEnumerable<Claim> GetClaim(string memberId)
        {
            return ValidateMemberId(memberId) ? _repository.GetClaim(memberId) : null;
        }

        public decimal GetClaimTotal(string memberId)
        {
            var claims = GetClaim(memberId);
            return claims == null 
                ? (decimal)0.0
                : claims.Select(claim => claim.Amount)
                  .ToList()
                  .Sum();
        }

        // Added input validation to avoid vulnerbility
        protected bool ValidateProductId(string productId) => productId.Contains("ABC");

        // Added input validation to avoid vulnerbility
        protected bool ValidateMemberId(string memberId) => memberId.StartsWith("X0");
    }
}