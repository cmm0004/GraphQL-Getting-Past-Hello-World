﻿using System.ComponentModel.DataAnnotations;

namespace GraphQLValidation.Data.Entities
{
    public class Loan
    {
        [Key]
        // this would normally be a Guid, but the examples are much easy to follow along with if its an string
        public string Id { get; set; }
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