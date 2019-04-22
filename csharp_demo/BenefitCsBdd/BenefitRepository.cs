using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BenefitCsBdd
{
    public interface IBenefitRepository
    {
        IEnumerable<Deductible> GetDeductible(string productId);
        OopMax GetOopMax(string productId);
        IEnumerable<Claim> GetClaim(string memberId);
    }

    public class BenefitRepository : IBenefitRepository
    {
        private readonly BenefitDbContext _context;

        public BenefitRepository(BenefitDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Deductible> GetDeductible(string productId)
        {
            //return _context.Deductibles
            //    .Where(deduct => deduct.ProductId.ToLower().Equals(productId.ToLower().Trim()))
            //    .OrderBy(deduct => deduct.Level)
            //    .ToList();
            throw new NotImplementedException();
        }

        public OopMax GetOopMax(string productId)
        {
            //return _context.Deductibles
            //    .Where(deduct => deduct.ProductId.ToLower().Equals(productId.ToLower().Trim()))
            //    .OrderBy(deduct => deduct.Level)
            //    .ToList();
            throw new NotImplementedException();
        }

        public IEnumerable<Claim> GetClaim(string memberId)
        {
            return _context.Claims
                .Where(claim => claim.MemberId.ToLower() == memberId.ToLower())
                .ToList();
        }
    }
}