using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeShareAPI.Model
{
    public class MedicalHistory
    {
        [Key] public int Id { get; set; }
        [Required] public int PatientId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public int AppointmentId { get; set; }

        public List<Allergie> Allergies { get; set; }
        public List<PreviousMedicalCondition> PreviousMedicalConditions { get; set; }
        public List<Medication> Medications { get; set; }
        public List<SurgicalHistory> SurgicalHistory { get; set; }
        public List<DiagnosticTest> DiagnosticTest { get; set; }
    }

    public class Allergie
    { 
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }

        public int MedicalHistoryId { get; set; }
        [ForeignKey(nameof(MedicalHistoryId))] public MedicalHistory MedicalHistory { get; set; }
    }

    public class PreviousMedicalCondition
    {
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        public int MedicalHistoryId { get; set; }
        [ForeignKey(nameof(MedicalHistoryId))] public MedicalHistory MedicalHistory { get; set; }
    }

    public class Medication
    {
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }
        [Required] public string Dosage { get; set; }
        [Required] public string Frequency { get; set; }
        [Required] public bool IsCurrent { get; set; } = false;

        public int MedicalHistoryId { get; set; }
        [ForeignKey(nameof(MedicalHistoryId))] public MedicalHistory MedicalHistory { get; set; }
    }

    public class SurgicalHistory
    {
        [Key] public int Id { get; set; }

        [Required] public string ProceduresName { get; set; }
        [Required] public DateTime ProceduresDate { get; set; }
        [Required] public string DoctorName { get; set; }
        [Required] public string HospitalName { get; set; }

        public int MedicalHistoryId { get; set; }
        [ForeignKey(nameof(MedicalHistoryId))] public MedicalHistory MedicalHistory { get; set; }
    }
    
    public class DiagnosticTest
    {
        [Key] public int Id { get; set; }
        [Required] public string LabTestName { get; set; }
        [Required] public DateTime LabTestDate { get; set; }
        [Required] public string Result { get; set; }
        [Required] public string Image { get; set; }
        [Required] public string ImageFindings { get; set; }

        public int MedicalHistoryId { get; set; }
        [ForeignKey(nameof(MedicalHistoryId))] public MedicalHistory MedicalHistory { get; set; }
    }
}
