using System.ComponentModel.DataAnnotations;
using VendorManagement.Models.Entities;

namespace VendorManagement.Models.ViewModels
{
    public class AddAuditViewModel
    {
        [Required]
        public int VendorId { get; set; }
        [Required]
        public string VendorName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime AuditDate { get; set; } = DateTime.Now;
        public IEnumerable<VendorSharedData> VendorSharedData { get; set; }
        [Required]
        public List<int> SelectedVendorSharedDataIds { get; set; }

    }
}
