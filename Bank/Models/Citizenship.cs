using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Citizenship
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }
    }
}
