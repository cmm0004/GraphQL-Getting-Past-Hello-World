using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLValidation.Data.Entities
{
    public class CheckingAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        //normally this would be a guid, but its much easier to read the examples if this is an int
        public int UserProductId { get; set; }
        public string FinancialInstitution { get; set; } = "";
        public decimal CurrentBalance { get; set; }
        public double FeePerMonth { get; set; }
        public bool IsGoodStanding { get; set; }
    }
}