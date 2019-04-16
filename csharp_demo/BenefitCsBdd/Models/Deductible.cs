namespace BenefitCsBdd
{
    public class Deductible
    {
        public int DeductibleId { get; set; }
        public string ProductId { get; set; }
        public int Tier { get; set; }
        public decimal Amount { get; set; }
    }
}