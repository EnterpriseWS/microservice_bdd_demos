using System;
using TechTalk.SpecFlow;
using FluentAssertions;
using BenefitCsBdd;
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
        public DeductibleController _deductController = null;
        public IEnumerable<Deductible> _deductibles = null;

        //protected Mock<IBenefitRepository> _mockRepository = new Mock<IBenefitRepository>();
        //protected List<Deductible> _mockDeduct = new List<Deductible>();
        //protected OopMax _mockOop = null;
        //protected IEnumerable<Deductible> _deduct;
        //protected SqliteConnection _conn;
        //protected DbContextOptions<BenefitDbContext> _options;
        //protected decimal _claimTotal;

        [Given(@"The medical benefit has level_one deductible and level_two deductible")]
        public void GivenTheMedicalBenefitHasLevel_OneDeductibleAndLevel_TwoDeductible()
        {
            var deductibles = new List<Deductible>()
            {
                new Deductible() {ProductId = "ABC00001", Tier = 1, Amount = 250 },
                new Deductible() {ProductId = "ABC00001", Tier = 2, Amount = 350 }
            };
            var mockBenefit = new Mock<IBenefit>();
            mockBenefit.Setup(deduct => deduct.GetDeductible("X0001")).Returns(deductibles);

            _deductController = new DeductibleController(mockBenefit.Object);
        }

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
                    var writeCount = context.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
        }

        [Given(@"the max OOP amount is five hundred dollars")]
        public void GivenTheMaxOOPAmountIsFiveHundredDollars()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I inquire the deductible amount")]
        public void WhenIInquireTheDeductibleAmount()
        {
            try
            {
                _deductibles = _deductController.Get("X0001");
                _deductibles.Should().NotBeNull();
            }
            catch (Exception e)
            {
                e.Message.Should().BeNullOrEmpty();
            }
        }

        [When(@"I inquire the max OOP amount")]
        public void WhenIInquireTheMaxOOPAmount()
        {
            ScenarioContext.Current.Pending();
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
                    _claimTotal = benefit.GetClaimTotal("X0002");
                }
            }
            catch (Exception e)
            {

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
            ScenarioContext.Current.Pending();
        }

        [Then(@"the result should be either a sum of claim amounts or its max OOP amount as the table below")]
        public void ThenTheResultShouldBeEitherASumOfClaimAmountsOrItsMaxOOPAmountAsTheTableBelow(Table table)
        {
            _claimTotal.Should().Be((decimal)720.0);
        }
    }
}
