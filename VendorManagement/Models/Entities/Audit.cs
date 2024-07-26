namespace VendorManagement.Models.Entities
{
    public class Audit
    {
        public int AuditId { get; set; }
        public DateTime AuditDate { get; set; }

        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        public virtual ICollection<VendorSharedData> VendorSharedData { get; set; }
        public Audit()
        {
            VendorSharedData = new List<VendorSharedData>();
        }
    }
}
