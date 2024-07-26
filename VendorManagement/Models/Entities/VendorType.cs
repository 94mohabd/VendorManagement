namespace VendorManagement.Models.Entities
{
    public class VendorType
    {
        public int VendorTypeId { get; set; }
        public string Type { get; set; } // e.g., Hardware, Software, Services

        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
