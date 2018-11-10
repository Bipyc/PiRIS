using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        public string PassportSeries { get; set; }
        [Required]
        public string PassportNumber { get; set; }
        [Required]
        public string WhoAssigned { get; set; }
        [Required]
        public string IdentityNumber { get; set; }
        [Required]
        public string PlaceBirth { get; set; }
        [Required]
        [ForeignKey("City.Id")]
        public int CurrentCityId { get; set; }
        [Required]
        public string CurrentAddress { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string WorkPlace { get; set; }
        public string WorkPosition { get; set; }
        [Required]
        [ForeignKey("City.Id")]
        public int RegistrationCityId { get; set; }
        [Required]
        public string RegistrationAddress { get; set; }
        [Required]
        [ForeignKey("MaritalStatus.Id")]
        public int MaritalStatusId { get; set; }
        [Required]
        [ForeignKey("Citizenship.Id")]
        public int CitizenshipId { get; set; }
        [Required]
        [ForeignKey("Disability.Id")]
        public int DisabilityId { get; set; }
        [Required]
        public bool IsRetired { get; set; }
        public decimal MonthRevenue { get; set; }
        [Required]
        public bool IsLiableForMilitaryService { get; set; }
    }
}
