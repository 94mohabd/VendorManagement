namespace VendorManagement.Models.Entities
{
    public class AuditCycle
    {
        public int AuditCycleId { get; set; }
        public string AuditCycleType { get; set; }
        public int AuditCyclePeriod { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
