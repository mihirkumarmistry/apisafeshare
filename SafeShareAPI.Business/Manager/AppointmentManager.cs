using Microsoft.EntityFrameworkCore;
using SafeShareAPI.Data;
using SafeShareAPI.Model;
using SafeShareAPI.Model.Common;

namespace SafeShareAPI.Business
{
    public class AppointmentManager : VariableManager
    {
        public List<Appointment> listData = null;

        public Exceptions Select()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.Appointments.ToList();
                listData.ForEach(d =>
                {
                    User doctor = defaultContext.Users.AsNoTracking().Where(u => u.Id == d.DoctorId).FirstOrDefault();
                    if (doctor != null)
                    {
                        d.DoctorName = $"{doctor.FirstName} {doctor.LastName}";
                    }
                });
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }

        public Exceptions Select(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.Appointments.Where(u => u.Id == data).ToList();
                listData.ForEach(d =>
                {
                    Patient patient = defaultContext.Patients.Where(p => p.Id == d.PatientId).FirstOrDefault();
                    if (patient != null)
                    {
                        d.AddressLine1 = patient.AddressLine1;
                        d.AddressLine2 = patient.AddressLine2;
                        d.City = patient.City;
                        d.State = patient.State;
                        d.Country = patient.Country;
                        d.ZipCode = patient.ZipCode;
                    }
                });
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }

        public Exceptions Save(Appointment data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (data.Id == 0)
                {
                    if (data.PatientId == 0)
                    {
                        Patient patient = new() {
                            Id = 0,
                            FirstName = data.FirstName,
                            LastName = data.LastName,
                            DateOfBirth = data.DateOfBirth,
                            Gender = data.Gender,
                            Contact = data.Contact,
                            AddressLine1 = data.AddressLine1,
                            AddressLine2 = data.AddressLine2,
                            City = data.City,
                            State = data.State,
                            Country = data.Country,
                            ZipCode = data.ZipCode,
                            ContactPersonName = "",
                            EmergencyContact = "",
                            InsuranceContact = "",
                            InsuranceProviderName = "",
                            MiddleName = "",
                            PolicyholderName = "",
                            PolicyNumber = "",
                            Relationship = ""
                        };
                        defaultContext.Patients.Add(patient);
                        defaultContext.SaveChanges();

                        data.PatientId = patient.Id;
                    }
                    defaultContext.Appointments.Add(data);
                }
                else { defaultContext.Appointments.Update(data); }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Delete(Appointment data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (defaultContext.Appointments.AsNoTracking().Where(d => d.Id == data.Id).Any())
                {
                    defaultContext.Appointments.Remove(data);
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
