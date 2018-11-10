using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        [ForeignKey("CurrencyType.Id")]
        public int CurrencyTypeId { get; set; }
        [Required]
        public decimal Debit { get; set; }
        [Required]
        public decimal Credit { get; set; }
        [Required]
        public decimal Saldo { get; set; }
        [Required]
        public bool IsClosed { get; set; }
        [Required]
        public string AccountName { get; set; }
        [ForeignKey("Contract.Id")]
        public int? ContractId { get; set; }
        [ForeignKey("Client.Id")]
        public int? ClientId { get; set; }
    }
}
