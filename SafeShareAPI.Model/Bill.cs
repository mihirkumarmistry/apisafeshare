using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SafeShareAPI.Model
{
    public class Bill
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        
        [Required] public float TaxPercentage { get; set; }
        [Required] public DateTime BillingDate { get; set; }
        [Required] public DateTime BillingDueDate { get; set; }

        public List<BillBreaskdown>? Breaskdowns { get; set; }
        
        public float Discount { get; set; }
        public float TaxAmount { get; set; }
        public float BillAmount { get; set; }
        public string InsuranceNumber { get; set; }
        public float FinalBillAmount { get; set; }
        public float InsuranceCoverageAmount { get; set; }

        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public float AmountPaid { get; set; }
        public string TransactionId { get; set; }
        public float BalanceDue { get; set; }
        public string PaymentStatus { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class BillBreaskdown
    {
        [Key] public int Id { get; set; }
        [Required] public string ServiceName { get; set; }
        [Required] public string Type { get; set; }
        [Required] public float Amount { get; set; }
        [Required] public int BillId { get; set; }

    }
}
