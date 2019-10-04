using System;
using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLValidation.Data.Entities
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int UserProductId { get; set; }
        public string Lender { get; set; } = "";
        public decimal CurrentBalance { get; set; }
        public decimal Principal { get; set; }
        public int TermMonths { get; set; }
        public double InterestRatePerYear { get; set; }
        public double FeePerMonth { get; set; }
        public bool IsGoodStanding { get; set; }
    }
}