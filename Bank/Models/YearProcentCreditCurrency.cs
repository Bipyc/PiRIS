using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    [Table("YearProcentCreditCurrencies")]
    public class YearProcentCreditCurrency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Credit.Id")]
        public int CreditId { get; set; }
        [Required]
        [ForeignKey("CurrencyType.Id")]
        public int CurrencyTypeId { get; set; }
        public CurrencyType CurrencyType { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public decimal MinAmount { get; set; }
        [Required]
        public decimal MaxAmount { get; set; }
    }
}

