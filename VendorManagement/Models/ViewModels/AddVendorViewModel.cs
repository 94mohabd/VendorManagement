using System.ComponentModel.DataAnnotations;
using VendorManagement.Models.Entities;

namespace VendorManagement.Models.ViewModels
{
    public class AddVendorViewModel
    {
        public int VendorId { get; set; }
        [Required]
        [StringLength(100)]
        public string VendorName { get; set; }
        [Required]
        [EmailAddress]
        public string VendorContactEmail { get; set; }
        [Required]
        [EmailAddress]
        public string DepartmentalOwnerEmail { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ContractDate { get; set; } = DateTime.Now;
        [Required]
        [DataType(DataType.Date)]
        public DateTime AuditDate { get; set; } = DateTime.Now;
        public IEnumerable<ContractCycle> ContractCycles { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
        [Required]
        public int SelectedContractCycleId { get; set; }
        public IEnumerable<AuditCycle> AuditCycles { get; set; }
        [Required]
        public int SelectedAuditCycleId { get; set; }
        public IEnumerable<CriticalityLevel> CriticalityLevels { get; set; }
        [Required]
        public int SelectedCriticalityLevelId { get; set; }
        public IEnumerable<VendorType> VendorTypes { get; set; }
        [Required]
        public int SelectedVendorTypeId{ get; set; }
        public IEnumerable<VendorSharedData> VendorSharedData { get; set; }
        [Required]
        public List<int> SelectedVendorSharedDataIds { get; set; }
        public ICollection<Audit>? Audits { get; set; }
        public ICollection<AssignedAssessment>? AssignedAssessments { get; set; }
        public int SelectedVendorId { get; set; }
    }
}
