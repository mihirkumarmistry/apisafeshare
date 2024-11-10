using SafeShareAPI.Data;
using SafeShareAPI.Model.Common;
using SafeShareAPI.Model;
using Microsoft.EntityFrameworkCore;

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
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }

        public Exceptions Select(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.Appointments.Where(u => u.Id == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }

        public Exceptions Save(Appointment data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (data.Id == 0) { defaultContext.Appointments.Add(data); }
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
