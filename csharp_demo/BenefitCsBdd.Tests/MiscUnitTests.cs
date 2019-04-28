using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BenefitCsBdd;
using Moq;
using System.Linq;
using System.Collections.Generic;
using FluentAssertions;
using System.Configuration;
using System.Collections.Specialized;

namespace BenefitCsBdd.Tests
{
    [TestClass]
    public class MiscUnitTests
    {
        private Mock<IBenefitRepository> _mockBenefitRepo;

        public MiscUnitTests()
        {
            //| MemberId  | ProductId | Tier  | ClaimDesc     | Amount |
            //| X0001     | ABC00001  | 1     | Office Visit  | 100.00 |
            //| X0001     | ABC00001  | 1     | Blood Test    | 50.00 |
            //| X0001     | ABC00001  | 2     | X - Ray       | 75.00 |
            //| X0001     | ABC00001  | 2     | Specialist Visit | 350.00 |

            var deductibles = new List<Deductible>()
            {
                new Deductible() {ProductId = "ABC00001", Tier = 1, Amount = 250 },
                new Deductible() {ProductId = "ABC00001", Tier = 2, Amount = 350 }
            };
            var oopMax = new OopMax() { ProductId = "ABC00001", Amount = 500 };
            var claims = new List<Claim>()
            {
                new Claim() {MemberId = "X0001", ProductId = "ABC00001", Tier = 1, Amount = 100 },
                new Claim() {MemberId = "X0001", ProductId = "ABC00001", Tier = 1, Amount = 50 },
                new Claim() {MemberId = "X0001", ProductId = "ABC00001", Tier = 2, Amount = 75 },
                new Claim() {MemberId = "X0001", ProductId = "ABC00001", Tier = 2, Amount = 350 },
            };
            _mockBenefitRepo = new Mock<IBenefitRepository>();
            _mockBenefitRepo.Setup(benefit => benefit.GetDeductibles("ABC00001")).Returns(deductibles);
            _mockBenefitRepo.Setup(benefit => benefit.GetOopMax("ABC00001")).Returns(oopMax);
            _mockBenefitRepo.Setup(benefit => benefit.GetClaims("X0001")).Returns(claims);
        }

        [TestMethod]
        public void TestConfigSectionGroup()
        {
            var _wellknownSites = new List<string>();
            var settings = (NameValueCollection)ConfigurationManager.GetSection("wellknownSite/sites");
            foreach (string key in settings.AllKeys)
                _wellknownSites.Add(settings[key].ToString().Trim());

            _wellknownSites[0].Should().Be("https://www.google.com");
            _wellknownSites[1].Should().Be("https://www.yahoo.com");
            _wellknownSites[2].Should().Be("https://www.bing.com");
        }

        [TestMethod]
        public void Test_MultiTierBenefits_GetDeductibles()
        {
            var multiTierBenefit = new MultiTierBenefit(_mockBenefitRepo.Object);
            var benefitDeductibles = multiTierBenefit.GetDeductibles("ABC00001");
            benefitDeductibles.Count().Should().Be(2);
        }

        [TestMethod]
        public void Test_MultiTierBenefits_GetOopMax()
        {
            var multiTierBenefit = new MultiTierBenefit(_mockBenefitRepo.Object);
            var benefitOopMax = multiTierBenefit.GetOopMax("ABC00001");
            benefitOopMax.Amount.Should().Be(500);
        }

        [TestMethod]
        public void Test_MultiTierBenefits_GetClaims()
        {
            var multiTierBenefit = new MultiTierBenefit(_mockBenefitRepo.Object);
            var benefitClaims = multiTierBenefit.GetClaims("X0001");
            benefitClaims.Count().Should().Be(4);
        }

        [TestMethod]
        public void Test_MultiTierBenefits_GetOopMaxMet()
        {
            var multiTierBenefit = new MultiTierBenefit(_mockBenefitRepo.Object);
            var benefitOopMaxMet = multiTierBenefit.GetOopMaxMet("X0001");
            benefitOopMaxMet.Should().Be(500);
        }
    }
}
