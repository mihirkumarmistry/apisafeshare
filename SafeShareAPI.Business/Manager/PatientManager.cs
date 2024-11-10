using Microsoft.EntityFrameworkCore;
using SafeShareAPI.Data;
using SafeShareAPI.Model;
using SafeShareAPI.Model.Common;

namespace SafeShareAPI.Business
{
    public class PatientManager : VariableManager
    {
        public List<Patient> listData = null;

        public Exceptions Select()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.Patients.ToList();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }

        public Exceptions Select(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.Patients.Where(u => u.Id == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }

        public Exceptions Save(Patient data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (data.Id == 0 && !defaultContext.Patients.AsNoTracking().Where(d => d.Contact == data.Contact).Any()) { defaultContext.Patients.Add(data); }
                else if (data.Id != 0 && !defaultContext.Patients.AsNoTracking().Where(d => d.Contact == data.Contact && d.Id != data.Id).Any()) { defaultContext.Patients.Update(data); }
                else { exceptions = Exceptions.AlreadyExists; }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Delete(Patient data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (User.IsUniversal) { defaultContext.Patients.Remove(data); }
                else { exceptions = Exceptions.NotPermitted; }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
    }
}
