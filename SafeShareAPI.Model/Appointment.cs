using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeShareAPI.Model
{
    public class Appointment
    {
        [Key] public int Id { get; set; }
        [Required] public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public string Contact { get; set; }
        [Required] public string AppointmentType { get; set; }
        [Required] public DateTime AppointmentDate { get; set; }
        [Required] public DateTime AppointmentTime { get; set; }
        [Required] public string Reason { get; set; }


        [Required] public int PatientId { get; set; }
        [NotMapped] [ForeignKey(nameof(PatientId))] public Patient? Patient { get; set; }
        [Required] public int DoctorId { get; set; }
        [NotMapped] [ForeignKey(nameof(DoctorId))] public User? User { get; set; }

        [NotMapped] public string? AddressLine1 { get; set; }
        [NotMapped] public string? AddressLine2 { get; set; }
        [NotMapped] public string? City { get; set; }
        [NotMapped] public string? State { get; set; }
        [NotMapped] public string? Country { get; set; }
        [NotMapped] public string? ZipCode { get; set; }

        [NotMapped] public string? DoctorName { get; set; }

    }
}
