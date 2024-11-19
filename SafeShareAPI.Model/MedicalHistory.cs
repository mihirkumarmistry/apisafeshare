using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeShareAPI.Model
{
    public class MedicalHistory
    {
        [Key] public int Id { get; set; }
        [Required] public int PatientId { get; set; }
        [Required] public string Title { get; set; }
        public string Detail { get; set; }

        [NotMapped] public List<Allergie>? Allergies { get; set; }
        [NotMapped] public List<PreviousMedicalCondition>? PreviousMedicalConditions { get; set; }
        [NotMapped] public List<Medication>? Medications { get; set; }
        [NotMapped] public List<SurgicalHistory>? SurgicalHistory { get; set; }
        [NotMapped] public List<DiagnosticTest>? DiagnosticTest { get; set; }

        [NotMapped] public string? PatientName { get; set; }
    }

    public class Allergie
    { 
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public int MedicalHistoryId { get; set; }
    }

    public class PreviousMedicalCondition
    {
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public int MedicalHistoryId { get; set; }
    }

    public class Medication
    {
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }
        [Required] public string Dosage { get; set; }
        [Required] public string Frequency { get; set; }
        [Required] public bool IsCurrent { get; set; } = false;
        [Required] public int MedicalHistoryId { get; set; }
    }

    public class SurgicalHistory
    {
        [Key] public int Id { get; set; }

        [Required] public string ProceduresName { get; set; }
        [Required] public DateTime ProceduresDate { get; set; }
        [Required] public string DoctorName { get; set; }
        [Required] public string HospitalName { get; set; }
        [Required] public int MedicalHistoryId { get; set; }

    }

    public class DiagnosticTest
    {
        [Key] public int Id { get; set; }
        [Required] public string LabTestName { get; set; }
        [Required] public DateTime LabTestDate { get; set; }
        [Required] public string Result { get; set; }
        [Required] public string Image { get; set; }
        [Required] public string ImageFindings { get; set; }
        [Required] public int MedicalHistoryId { get; set; }
    }
}
