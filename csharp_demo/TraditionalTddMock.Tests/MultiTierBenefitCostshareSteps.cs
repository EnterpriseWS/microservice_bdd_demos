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

namespace BenefitCsBdd.Tests.Steps
{
    [Binding]
    public class MultiTierBenefitCostshareSteps
    {
        protected Mock<IBenefitRepository> _mockRepository = new Mock<IBenefitRepository>();
        protected List<Deductible> _mockDeduct = new List<Deductible>();
        protected IEnumerable<Deductible> _deduct;
        protected SqliteConnection _conn;
        protected DbContextOptions<BenefitDbContext> _options;
        protected decimal _claimTotal;

        [Given(@"The medical benefit has level_one deductible and level_two deductible")]
        public void GivenTheMedicalBenefitHasLevel_OneDeductibleAndLevel_TwoDeductible()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"The table below contains a sample of insured member medical claims for all tiers")]
        public void GivenTheTableBelowContainsASampleOfInsuredMemberMedicalClaimsForAllTiers(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"the max OOP amount is five hundred dollars")]
        public void GivenTheMaxOOPAmountIsFiveHundredDollars()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should output level_one and level_two deductible")]
        public void ThenTheResultShouldOutputLevel_OneAndLevel_TwoDeductible()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be either a sum of claim amounts or its max OOP amount as the table below")]
        public void ThenTheResultShouldBeEitherASumOfClaimAmountsOrItsMaxOOPAmountAsTheTableBelow(Table table)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
