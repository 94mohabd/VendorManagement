using System.ComponentModel.DataAnnotations;
using VendorManagement.Models.Entities;

namespace VendorManagement.Models.ViewModels
{
    public class VendorDetailsViewModel
    {
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public IEnumerable<Audit> Audits { get; set; }
        public IEnumerable<AssignedAssessment> AssignedAssessments { get; set; }
        public DateTime AssignmentDate { get; set; }
        public IEnumerable<Assessment> Assessments { get; set; }
        [Required] 
        public int SelectedVendorId { get; set; }
        public int SelectedAssessmentId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime AuditDate { get; set; } = DateTime.Now;
        [Required]
        [DataType(DataType.Date)]
        public DateTime NewAuditDate { get; set; } = DateTime.Now;
        [Required] 
        public List<int> SelectedVendorSharedDataIds { get; set; }
        public IEnumerable<VendorSharedData> VendorSharedData { get; set; }

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
        public DateTime ContractDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime SuditDate { get; set; }
        [Required]
        public int SelectedContractCycleId { get; set; }
        public IEnumerable<ContractCycle> ContractCycles { get; set; }
        [Required]
        public int SelectedAuditCycleId { get; set; }
        public IEnumerable<AuditCycle> AuditCycles { get; set; }
        public IEnumerable<CriticalityLevel> CriticalityLevels { get; set; }
        public IEnumerable<VendorType> VendorTypes { get; set; }
        [Required]
        public int SelectedCriticalityLevelId { get; set; }        
        [Required]
        public int SelectedVendorTypeId { get; set; }             
    }
}
