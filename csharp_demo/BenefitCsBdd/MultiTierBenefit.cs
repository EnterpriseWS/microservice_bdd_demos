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

        public OopMax GetOopMax(string productId)
        {
            return ValidateProductId(productId) ? _repository.GetOopMax(productId) : null;
        }

        public IEnumerable<Claim> GetClaim(string memberId)
        {
            return ValidateMemberId(memberId) ? _repository.GetClaim(memberId) : null;
        }

        public decimal GetOopMaxMet(string memberId)
        {
            var claims = GetClaim(memberId);
            if (claims != null && claims.Any())
            {
                var oopMaxAmount = GetOopMax(claims.First().ProductId).Amount;
                var claimsSum = claims.Select(claim => claim.Amount)
                                .ToList()
                                .Sum();
                return claimsSum > oopMaxAmount ? oopMaxAmount : claimsSum;
            }
            else
                return 0;
        }

        // Added input validation to avoid vulnerbility
        protected bool ValidateProductId(string productId) => productId.Contains("ABC");

        // Added input validation to avoid vulnerbility
        protected bool ValidateMemberId(string memberId) => memberId.StartsWith("X0");
    }
}