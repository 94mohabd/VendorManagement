namespace VendorManagement.Models.Entities
{
    public class CriticalityLevel
    {
        public int CriticalityLevelId { get; set; }
        public string Level { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
