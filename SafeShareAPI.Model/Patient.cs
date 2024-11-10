using System.ComponentModel.DataAnnotations;

namespace SafeShareAPI.Model
{
    public class Patient
    {
        [Key] public int Id { get; set; }
        [Required] public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public string Contact { get; set; }

        [Required] public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required] public string City { get; set; }
        [Required] public string State { get; set; }
        [Required] public string Country { get; set; }
        [Required] public string ZipCode { get; set; }
        
        public string EmergencyContact { get; set; }
        public string ContactPersonName { get; set; }
        public string Relationship { get; set; }
        
        public string InsuranceProviderName { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyholderName { get; set; }
        public string InsuranceContact { get; set; }
    }
}
