using VendorManagement.Models.Entities;

namespace VendorManagement.Models.ViewModels
{
    public class AddAuditCycleViewModel
    {
        public int AuditCycleId { get; set; }
        public string AuditCycleType { get; set; }   
        public IEnumerable<AuditCycle> AuditCycles { get; set; }
    }
}
