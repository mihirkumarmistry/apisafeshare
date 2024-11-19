using Microsoft.EntityFrameworkCore;
using SafeShareAPI.Data;
using SafeShareAPI.Model;
using SafeShareAPI.Model.Common;

namespace SafeShareAPI.Business
{
    public class CommonManager : VariableManager
    {
        public List<KeyVal> listData = null;
        public Exceptions DoctorList()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.Users.AsNoTracking().Where(d => d.UserTypeId == 2 && !d.IsDeleted).Select(s => new KeyVal 
                {
                    Key = $"{s.FirstName} {s.LastName}",
                    Value = s.Id
                }).ToList();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }

        public Exceptions AppointmentList()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.Appointments.AsNoTracking().OrderByDescending(d => d.AppointmentDate).Select(s => new KeyVal
                {
                    Key = $"{s.AppointmentType} | {s.FirstName} {s.LastName}",
                    Value = s.Id
                }).ToList();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }

        public Exceptions PatientList()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.Patients.AsNoTracking().Select(s => new KeyVal
                {
                    Key = $"{s.FirstName} {s.LastName}",
                    Value = s.Id
                }).ToList();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
    }
}
