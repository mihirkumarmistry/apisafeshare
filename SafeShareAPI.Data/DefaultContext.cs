using Microsoft.EntityFrameworkCore;
using SafeShareAPI.Model;
using static SafeShareAPI.Provider.ConnectionProvider;

namespace SafeShareAPI.Data
{
    public class DefaultContext : ContextTables { public DefaultContext(Connection connection) : base() { Connection = connection; } }
    public class ContextTables : ContextProvider
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<Allergie> Allergies { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<BillBreaskdown> BillBreaskdowns { get; set; }
        public DbSet<DiagnosticTest> DiagnosticTests { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<SurgicalHistory> SurgicalHistories { get; set; }
        public DbSet<PreviousMedicalCondition> PreviousMedicalConditions { get; set; }
    }
}
