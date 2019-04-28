using System;
using TechTalk.SpecFlow;
using FluentAssertions;
//using BenefitCsBdd;
using Moq;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow.Assist;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;
using BenefitCsBdd.Controllers;

namespace BenefitCsBdd.Tests.Steps
{
    [Binding]
    public class MultiTierBenefitCostshareSteps
    {
        public OopMaxController _oopMaxController = null;
        public DeductibleController _deductController = null;
        public IEnumerable<Deductible> _deductibles = null;
        public OopMax _oopMax = null;

        protected SqliteConnection _conn;
        protected DbContextOptions<BenefitDbContext> _options;
        protected decimal _claimTotalMember1;
        protected decimal _claimTotalMember2;

        [Given(@"The medical benefit has level_one deductible and level_two deductible")]
        public void GivenTheMedicalBenefitHasLevel_OneDeductibleAndLevel_TwoDeductible()
        {
            var deductibles = new List<Deductible>()
            {
                new Deductible() {ProductId = "ABC00001", Tier = 1, Amount = 250 },
                new Deductible() {ProductId = "ABC00001", Tier = 2, Amount = 350 }
            };
            var mockBenefit = new Mock<IBenefit>();
            mockBenefit.Setup(benefit => benefit.GetDeductibles("ABC00001")).Returns(deductibles);

            _deductController = new DeductibleController(mockBenefit.Object);
        }

        [Given(@"the max OOP amount is five hundred dollars")]
        [Given(@"The medical benefit has only one max OOP amount")]
        public void GivenTheMedicalBenefitHasOnlyOneMaxOOPAmount()
        {
            var oopMax = new OopMax() { ProductId = "ABC00001", Amount = 500 };
            var mockBenefit = new Mock<IBenefit>();
            mockBenefit.Setup(repository => repository.GetOopMax("ABC00001")).Returns(oopMax);

            _oopMaxController = new OopMaxController(mockBenefit.Object);
        }

        [Given(@"The table below contains a sample of insured member medical claims for all tiers")]
        public void GivenTheTableBelowContainsASampleOfInsuredMemberMedicalClaimsForAllTiers(Table table)
        {
            var claims = table.CreateSet<Claim>();
            _conn = new SqliteConnection("DataSource=:memory:");
            _options = new DbContextOptionsBuilder<BenefitDbContext>().UseSqlite(_conn).Options;

            try
            {
                _conn.Open();

                // Arrange
                using (var context = new BenefitDbContext(_options))
                {
                    // Make sure BenefitDbContext object has defined "Claim" table
                    // Create the schema and exit to force it to create immediately
                    context.Database.EnsureCreated();
                }

                // Action
                using (var context = new BenefitDbContext(_options))
                {
                    // Add records to Claim table for assertion
                    foreach (Claim claim in claims)
                    {
                        context.Claims.Add(claim);
                    }
                    var oopMax = new OopMax() { ProductId = "ABC00001", Amount = 500 };
                    context.OopMaxes.Add(oopMax);
                    var writeCount = context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                // Any exception occurs, it should be considered testing failed.
                e.Message.Should().BeNullOrEmpty();
            }
        }
        
        [When(@"I inquire the deductible amount")]
        public void WhenIInquireTheDeductibleAmount()
        {
            try
            {
                _deductibles = _deductController.Get("ABC00001");
                _deductibles.Should().NotBeNull();
            }
            catch (Exception e)
            {
                // Any exception occurs, it should be considered testing failed.
                e.Message.Should().BeNullOrEmpty();
            }
        }

        [When(@"I inquire the max OOP amount")]
        public void WhenIInquireTheMaxOOPAmount()
        {
            try
            {
                _oopMax = _oopMaxController.Get("ABC00001");
                _oopMax.Should().NotBeNull();
            }
            catch (Exception e)
            {
                // Any exception occurs, it should be considered testing failed.
                e.Message.Should().BeNullOrEmpty();
            }
        }

        [When(@"I inquire a member current OOP amount")]
        public void WhenIInquireAMemberCurrentOOPAmount()
        {
            try
            {
                // Action & Asserion
                using (var context = new BenefitDbContext(_options))
                {
                    var benefitDb = new BenefitRepository(context);
                    var benefit = new MultiTierBenefit(benefitDb);
                    _claimTotalMember1 = benefit.GetOopMaxMet("X0001");
                    _claimTotalMember2 = benefit.GetOopMaxMet("X0002");
                }
            }
            catch (Exception e)
            {
                // Any exception occurs, it should be considered testing failed.
                e.Message.Should().BeNullOrEmpty();
            }
            finally
            {
                if (_conn.State != ConnectionState.Closed)
                    _conn.Close();
                _conn.Dispose();
            }
        }

        [Then(@"the result should output level_one and level_two deductible")]
        public void ThenTheResultShouldOutputLevel_OneAndLevel_TwoDeductible()
        {
            _deductibles.Count<Deductible>().Should().Be(2);
        }

        [Then(@"the result should output one max OOP amount")]
        public void ThenTheResultShouldOutputOneMaxOOPAmount()
        {
            _oopMax.Amount.Should().Be(500);
        }

        [Then(@"the result should be either a sum of claim amounts or its max OOP amount as the table below")]
        public void ThenTheResultShouldBeEitherASumOfClaimAmountsOrItsMaxOOPAmountAsTheTableBelow(Table table)
        {
            _claimTotalMember1.Should().Be((decimal)375.0);
            _claimTotalMember2.Should().Be((decimal)500.0);
        }
    }
}
