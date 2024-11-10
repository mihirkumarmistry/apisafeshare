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
        [ForeignKey(nameof(PatientId))] public Patient Patient { get; set; }
        [Required] public int DoctorId { get; set; }
        [ForeignKey(nameof(DoctorId))] public User User { get; set; }
    }
}
