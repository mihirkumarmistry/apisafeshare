using Microsoft.EntityFrameworkCore;
using SafeShareAPI.Data;
using SafeShareAPI.Model;
using SafeShareAPI.Model.Common;

namespace SafeShareAPI.Business
{
    public class MedicalHistoryManager : VariableManager
    {
        public List<MedicalHistory> listData = null;

        public Exceptions Select()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.MedicalHistories.ToList();
                listData.ForEach(data =>
                {
                    data.PatientName = defaultContext.Patients.AsNoTracking().Where(d => d.Id == data.PatientId).Select(d => d.FirstName).FirstOrDefault();
                });
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Select(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { 
                using DefaultContext defaultContext = new(GetConnection()); 
                listData = defaultContext.MedicalHistories.Where(u => u.Id == data).ToList();
                listData.ForEach(u => {
                    u.Allergies = defaultContext.Allergies.AsNoTracking().Where(d => d.MedicalHistoryId == data).ToList();
                    u.PreviousMedicalConditions = defaultContext.PreviousMedicalConditions.AsNoTracking().Where(d => d.MedicalHistoryId == data).ToList();
                    u.Medications = defaultContext.Medications.AsNoTracking().Where(d => d.MedicalHistoryId == data).ToList();
                    u.SurgicalHistory = defaultContext.SurgicalHistories.AsNoTracking().Where(d => d.MedicalHistoryId == data).ToList();
                    u.DiagnosticTest = defaultContext.DiagnosticTests.AsNoTracking().Where(d => d.MedicalHistoryId == data).ToList();
                });
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Save(MedicalHistory data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                MedicalHistory medicalHistory = new()
                {
                    Id = 0,
                    Title = data.Title,
                    Detail = data.Detail,
                    PatientId = data.PatientId,
                };

                if (data.Id == 0 && !defaultContext.MedicalHistories.AsNoTracking().Where(d => d.PatientId == data.PatientId).Any())
                {
                    defaultContext.MedicalHistories.Add(data);
                }
                else if (data.Id != 0) { 
                    defaultContext.MedicalHistories.Update(data);
                }
                else { exceptions = Exceptions.AlreadyExists; }
                defaultContext.SaveChanges();
                listData = new List<MedicalHistory> { medicalHistory };
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Delete(MedicalHistory data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (User.IsUniversal) { defaultContext.MedicalHistories.Remove(data); }
                else { exceptions = Exceptions.NotPermitted; }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }

    }

    public class AllergieManager : VariableManager
    {
        public List<Allergie> listData = null;
        public Exceptions Select()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.Allergies.ToList();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Select(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.Allergies.Where(u => u.Id == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions SelectByMedicalId(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.Allergies.Where(u => u.MedicalHistoryId == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Save(Allergie data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (data.Id == 0) { defaultContext.Allergies.Add(data); }
                else { defaultContext.Allergies.Update(data); }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Delete(Allergie data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (defaultContext.Allergies.AsNoTracking().Where(d => d.Id == data.Id).Any())
                {
                    defaultContext.Allergies.Remove(data);
                }
                else
                {
                    exceptions = Exceptions.Failed;
                }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
    }

    public class PreviousMedicalConditionManager : VariableManager
    {
        public List<PreviousMedicalCondition> listData = null;
        public Exceptions Select()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.PreviousMedicalConditions.ToList();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Select(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.PreviousMedicalConditions.Where(u => u.Id == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }

        public Exceptions SelectByMedicalId(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.PreviousMedicalConditions.Where(u => u.MedicalHistoryId == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Save(PreviousMedicalCondition data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (data.Id == 0) { defaultContext.PreviousMedicalConditions.Add(data); }
                else { defaultContext.PreviousMedicalConditions.Update(data); }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Delete(PreviousMedicalCondition data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (defaultContext.PreviousMedicalConditions.AsNoTracking().Where(d => d.Id == data.Id).Any())
                {
                    defaultContext.PreviousMedicalConditions.Remove(data);
                }
                else
                {
                    exceptions = Exceptions.Failed;
                }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
    }

    public class MedicationManager : VariableManager
    {
        public List<Medication> listData = null;
        public Exceptions Select()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.Medications.ToList();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Select(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.Medications.Where(u => u.Id == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions SelectByMedicalId(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.Medications.Where(u => u.MedicalHistoryId == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Save(Medication data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (data.Id == 0) { defaultContext.Medications.Add(data); }
                else { defaultContext.Medications.Update(data); }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Delete(Medication data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (defaultContext.Medications.AsNoTracking().Where(d => d.Id == data.Id).Any())
                {
                    defaultContext.Medications.Remove(data);
                }
                else
                {
                    exceptions = Exceptions.Failed;
                }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
    }

    public class SurgicalHistoryManager : VariableManager
    {
        public List<SurgicalHistory> listData = null;
        public Exceptions Select()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.SurgicalHistories.ToList();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Select(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.SurgicalHistories.Where(u => u.Id == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions SelectByMedicalId(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.SurgicalHistories.Where(u => u.MedicalHistoryId == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Save(SurgicalHistory data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (data.Id == 0) { defaultContext.SurgicalHistories.Add(data); }
                else { defaultContext.SurgicalHistories.Update(data); }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Delete(SurgicalHistory data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (defaultContext.SurgicalHistories.AsNoTracking().Where(d => d.Id == data.Id).Any())
                {
                    defaultContext.SurgicalHistories.Remove(data);
                }
                else
                {
                    exceptions = Exceptions.Failed;
                }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
    }

    public class DiagnosticTestManager : VariableManager
    {
        public List<DiagnosticTest> listData = null;
        public Exceptions Select()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.DiagnosticTests.ToList();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Select(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.DiagnosticTests.Where(u => u.Id == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions SelectByMedicalId(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.DiagnosticTests.Where(u => u.MedicalHistoryId == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Save(DiagnosticTest data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (data.Id == 0) { defaultContext.DiagnosticTests.Add(data); }
                else { defaultContext.DiagnosticTests.Update(data); }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Delete(DiagnosticTest data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (defaultContext.DiagnosticTests.AsNoTracking().Where(d => d.Id == data.Id).Any())
                {
                    defaultContext.DiagnosticTests.Remove(data);
                }
                else
                {
                    exceptions = Exceptions.Failed;
                }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
    }
}
