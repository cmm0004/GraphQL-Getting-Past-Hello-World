using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLValidation.Data.Entities
{
    public class Cashflow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; } = "";
        public DateTime? TransactionDate { get; set; }
    }
}
