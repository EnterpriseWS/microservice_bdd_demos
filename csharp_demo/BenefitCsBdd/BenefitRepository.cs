using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BenefitCsBdd
{
    public interface IBenefitRepository
    {
        IEnumerable<Deductible> GetDeductibles(string productId);
        OopMax GetOopMax(string productId);
        IEnumerable<Claim> GetClaims(string memberId);
    }

    public class BenefitRepository : IBenefitRepository
    {
        private readonly BenefitDbContext _context;

        public BenefitRepository(BenefitDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Deductible> GetDeductibles(string productId)
        {
            try
            {
                return _context.Deductibles
                    .Where(deduct => deduct.ProductId.ToLower().Equals(productId.ToLower().Trim()))
                    .OrderBy(deduct => deduct.Tier)
                    .ToList();
            }
            catch
            {
                return null;
            }
        }

        public OopMax GetOopMax(string productId)
        {
            try
            {
                return _context.OopMaxes
                    .Where(oopMax => oopMax.ProductId.ToLower().Equals(productId.ToLower().Trim()))
                    .First();
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Claim> GetClaims(string memberId)
        {
            return _context.Claims
                .Where(claim => claim.MemberId.ToLower() == memberId.ToLower())
                .ToList();
        }
    }
}