using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        [ForeignKey("Account.Id")]
        public int AccountFromId { get; set; }
        [Required]
        [ForeignKey("Account.Id")]
        public int AccountToId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [ForeignKey("TransactionType.Id")]
        public int TransactionTypeId { get; set; }
    }
}
