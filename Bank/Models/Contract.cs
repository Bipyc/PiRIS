using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    [Table("Contracts")]
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime DateOfSign { get; set; }
        [Required]
        public DateTime DateOfEnd { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [ForeignKey("Deposit.Id")]
        public int? DepositId { get; set; }
        [ForeignKey("Credit.Id")]
        public int? CreditId { get; set; }
        [ForeignKey("CurrencyType.Id")]
        public int CurrencyTypeId { get; set; }
        [ForeignKey("Client.Id")]
        public int ClientId { get; set; }
    }
}
