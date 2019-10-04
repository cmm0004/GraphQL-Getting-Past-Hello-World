using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLValidation.Data.Entities
{
    public class CreditLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int UserProductId { get; set; }
        public string Creditor { get; set; } = "";
        public decimal CurrentBalance { get; set; }
        public decimal Line { get; set; }
        public double InterestPerMonth { get; set; }
        public double FeePerMonth { get; set; }
        public bool IsGoodStanding { get; set; }
    }
}
