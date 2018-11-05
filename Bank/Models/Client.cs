using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [RegularExpression("[а-яА-Яa-zA-Z]+")]
        public string MiddleName { get; set; }
        [Required]
        [RegularExpression("[а-яА-Яa-zA-Z]+")]
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
        [RegularExpression(@"^(?=(?:\D*\d){10,15}\D*$)\+?[0-9]{1,3}[\s-]?(?:\(0?[0-9]{1,5}\)|[0-9]{1,5})[-\s]?[0-9][\d\s-]{5,7}\s?(?:x[\d-]{0,4})?$")]
        public string HomePhone { get; set; }
        [RegularExpression(@"^(?=(?:\D*\d){10,15}\D*$)\+?[0-9]{1,3}[\s-]?(?:\(0?[0-9]{1,5}\)|[0-9]{1,5})[-\s]?[0-9][\d\s-]{5,7}\s?(?:x[\d-]{0,4})?$")]
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string WorkPlace { get; set; }
        public string WorkPosition { get; set; }
        [Required]
        [ForeignKey("City.Id")]
        public string RegistrationCityId { get; set; }
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
