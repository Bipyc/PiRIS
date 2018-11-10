using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    [Table("Deposits")]
    public class Deposit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int MinExpirationTerm { get; set; }
        [Required]
        public int MaxExpirationTerm { get; set; }

        public List<YearProcentDepositCurrency> CurrencyTypes { get; set; }
    }
}
