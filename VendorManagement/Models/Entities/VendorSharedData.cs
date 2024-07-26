namespace VendorManagement.Models.Entities
{
    public class VendorSharedData
    {
        public int VendorSharedDataId { get; set; }       
        public string SharedData { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<Vendor> Vendors { get; set; }
        public virtual ICollection<Audit> Audits { get; set; }
    }
}
