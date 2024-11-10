using Microsoft.EntityFrameworkCore;
using SafeShareAPI.Data;
using SafeShareAPI.Model;
using SafeShareAPI.Model.Common;
using SafeShareAPI.Provider;

namespace SafeShareAPI.Business
{
    public class UserManager : VariableManager
    {
        public List<User> listData = null;
        public Exceptions Select()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.Users.Include(u => u.UserType).Where(u => !u.IsDeleted).ToList();
                listData.ForEach(item => { item.UserTypeName = item.UserType.Name; });
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Select(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { using DefaultContext defaultContext = new(GetConnection()); listData = defaultContext.Users.Where(u => !u.IsDeleted && u.Id == data).ToList(); }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Save(User data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (data.Id == 0 && !defaultContext.Users.AsNoTracking().Where(d => d.Username == data.Username).Any()) { data.Password = EncryptionProvider.Encrypt(data.Password); defaultContext.Users.Add(data); }
                else if (data.Id != 0 && !defaultContext.Users.AsNoTracking().Where(d => d.Username == data.Username && d.Id != data.Id).Any())
                {
                    data.IsUniversal = defaultContext.Users.AsNoTracking().Where(d => d.Id == data.Id).FirstOrDefault().IsUniversal;
                    if (!IsOldPassword(data)) { data.Password = EncryptionProvider.Encrypt(data.Password); }
                    defaultContext.Users.Update(data);
                }
                else { exceptions = Exceptions.AlreadyExists; }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Delete(User data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                data.IsDeleted = true; if (User.IsUniversal) { defaultContext.Users.Remove(data); } else { defaultContext.Users.Update(data); }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public bool IsOldPassword(User data)
        {
            bool isSuccess = true;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                User user = defaultContext.Users.AsNoTracking().Where(d => d.Username == data.Username || d.Id == data.Id).FirstOrDefault();
                if (user != null && !FunctionProvider.IsEqualString(user.Password, data.Password)) { isSuccess = false; }
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); }
            return isSuccess;
        }
    }
}
