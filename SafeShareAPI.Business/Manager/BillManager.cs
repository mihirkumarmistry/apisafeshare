using SafeShareAPI.Data;
using SafeShareAPI.Model.Common;
using SafeShareAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace SafeShareAPI.Business
{
    public class BillManager : VariableManager
    {
        public List<Bill> listData = null;

        public Exceptions Select()
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                listData = defaultContext.Bills.AsNoTracking().Where(d => !d.IsDeleted).ToList();
                if (listData.Count > 0)
                {
                    listData.ForEach(u => {
                        u.Breaskdowns = defaultContext.BillBreaskdowns.AsNoTracking().Where(d => d.BillId == u.Id).ToList();
                    });
                }
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }

        public Exceptions Select(int data)
        {
            Exceptions exceptions = Exceptions.Success;
            try { 
                using DefaultContext defaultContext = new(GetConnection()); 
                listData = defaultContext.Bills.AsNoTracking().Where(u => u.Id == data && !u.IsDeleted).ToList();
                if (listData.Count > 0)
                {
                    listData.ForEach(u => {
                        u.Breaskdowns = defaultContext.BillBreaskdowns.AsNoTracking().Where(d => d.BillId == u.Id).ToList();
                    });
                }
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), null); exceptions = Exceptions.Failed; }
            return exceptions;
        }

        public Exceptions Save(Bill data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());

                data.BillAmount = data.Breaskdowns.Sum(d => d.Amount);
                float taxableAmount = data.BillAmount - data.Discount - data.InsuranceCoverageAmount;
                data.TaxAmount = (taxableAmount * data.TaxPercentage) / 100;
                data.FinalBillAmount = taxableAmount + data.TaxAmount;
                
                Bill bill = new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    PatientId = data.PatientId,
                    AmountPaid = data.AmountPaid,
                    AppointmentId = data.AppointmentId,
                    BalanceDue = data.BalanceDue,
                    BillAmount = data.AmountPaid,
                    BillingDate = data.BillingDate,
                    BillingDueDate = data.BillingDueDate,
                    Discount = data.Discount,
                    FinalBillAmount = data.FinalBillAmount,
                    InsuranceCoverageAmount = data.InsuranceCoverageAmount,
                    InsuranceNumber = data.InsuranceNumber,
                    PaymentDate = data.PaymentDate,
                    PaymentMode = data.PaymentMode,
                    PaymentStatus = data.PaymentStatus,
                    TaxAmount = data.TaxAmount,
                    TaxPercentage = data.TaxPercentage,
                    TransactionId = data.TransactionId,
                    Breaskdowns = []
                };

                if (data.Id == 0) {
                    defaultContext.Bills.Add(bill); 
                    defaultContext.SaveChanges();
                }
                else { 
                    defaultContext.Bills.Update(bill);
                    defaultContext.BillBreaskdowns.RemoveRange(defaultContext.BillBreaskdowns.Where(d => d.Id == bill.Id).ToList());
                    defaultContext.SaveChanges();
                }

                List<BillBreaskdown> billBreakdown = data.Breaskdowns.Select(d => new BillBreaskdown
                {
                    Id = 0,
                    ServiceName = d.ServiceName,
                    Amount = d.Amount,
                    Type = d.Type,
                    BillId = bill.Id
                }).ToList();

                defaultContext.BillBreaskdowns.AddRange(billBreakdown);
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
        public Exceptions Delete(Bill data)
        {
            Exceptions exceptions = Exceptions.Success;
            try
            {
                using DefaultContext defaultContext = new(GetConnection());
                if (User.IsUniversal) { data.IsDeleted = true; defaultContext.Bills.Update(data); }
                else { exceptions = Exceptions.NotPermitted; }
                defaultContext.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(Convert.ToString(ex), data); exceptions = Exceptions.Failed; }
            return exceptions;
        }
    }
}
