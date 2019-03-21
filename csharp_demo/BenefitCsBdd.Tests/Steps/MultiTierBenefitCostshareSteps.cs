using System;
using TechTalk.SpecFlow;

namespace BenefitCsBdd.Tests.Steps
{
    [Binding]
    public class MultiTierBenefitCostshareSteps
    {
        [Given(@"The medical benefit has level_one deductible and level_two deductible")]
        public void GivenTheMedicalBenefitHasLevel_OneDeductibleAndLevel_TwoDeductible()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"The medical benefit has only one max OOP amount")]
        public void GivenTheMedicalBenefitHasOnlyOneMaxOOPAmount()
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
        
        [When(@"I inquire the deductible amount")]
        public void WhenIInquireTheDeductibleAmount()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I inquire the max OOP amount")]
        public void WhenIInquireTheMaxOOPAmount()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I inquire a member current OOP amount")]
        public void WhenIInquireAMemberCurrentOOPAmount()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should output level_one and level_two deductible")]
        public void ThenTheResultShouldOutputLevel_OneAndLevel_TwoDeductible()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should output one max OOP amount")]
        public void ThenTheResultShouldOutputOneMaxOOPAmount()
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
