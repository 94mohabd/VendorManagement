using VendorManagement.Models.Entities;

namespace VendorManagement.Models.ViewModels
{
    public class AddVendorTypeViewModel
    {
        public int VendorTypeId { get; set; }
        public string Type { get; set; } // e.g., Hardware, Software, Services
        public IEnumerable<VendorType> VendorTypes { get; set; }
    }
}
