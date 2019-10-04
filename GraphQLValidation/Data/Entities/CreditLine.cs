using System.ComponentModel.DataAnnotations;

namespace GraphQLValidation.Data.Entities
{
    public class CreditLine
    {
        [Key]
        // this would normally be a Guid, but the examples are much easy to follow along with if its an string
        public string Id { get; set; }
        public int UserProductId { get; set; }
        public string Creditor { get; set; } = "";
        public decimal CurrentBalance { get; set; }
        public decimal Line { get; set; }
        public double InterestPerMonth { get; set; }
        public double FeePerMonth { get; set; }
        public bool IsGoodStanding { get; set; }
    }
}