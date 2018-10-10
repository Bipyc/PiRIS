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
        public int Id { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string WhoAssigned { get; set; }
        public string IdentityNumber { get; set; }
        public string PlaceBirth { get; set; }
        [ForeignKey("City.Id")]
        public int CurrentCityId { get; set; }
        public string CurrentAddress { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string WorkPlace { get; set; }
        public string WorkPosition { get; set; }
        [ForeignKey("City.Id")]
        public string RegistrationCityId { get; set; }
        public string RegistrationAddress { get; set; }
        [ForeignKey("MaritalStatus.Id")]
        public int MaritalStatusId { get; set; }
        [ForeignKey("Citizenship.Id")]
        public int CitizenshipId { get; set; }
        [ForeignKey("Disability.Id")]
        public int DisabilityId { get; set; }
        public bool IsRetired { get; set; }
        public decimal MonthRevenue { get; set; }
        public bool IsLiableForMilitaryService { get; set; }
    }
}
