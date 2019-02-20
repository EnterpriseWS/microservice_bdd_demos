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
    public class MultiTierBenefitTddMock
    {
        [TestMethod]
        public void TestGetDeductibleWithValidProductId()
        {
            // ----------------------
            // Test the happy path
            // ----------------------
            // Setup
            var mockRepository = new Mock<IBenefitRepository>();
            var mockDeduct = new List<Deductible>();
            mockDeduct.Add(new Deductible() { Level = 1, Amount = 2000 });
            mockDeduct.Add(new Deductible() { Level = 2, Amount = 3000 });
            mockRepository.Setup(repository => repository.GetDeductible("ABC00001")).Returns(mockDeduct);

            //Execute
            var benefit = new MultiTierBenefit(mockRepository.Object);
            var deduct = benefit.GetDeductible("ABC00001");

            //Assertion
            deduct.FirstOrDefault<Deductible>().Level.Should().Be(1);
            deduct.FirstOrDefault<Deductible>().Amount.Should().Be(2000);
        }

        [TestMethod]
        public void TestGetDeductibleWithInvalidProductId()
        {
            // ----------------------
            // TODO: Test the failed path
            // ----------------------
            int result = 1;

            result.Should().NotBe(2);
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
    }
}
