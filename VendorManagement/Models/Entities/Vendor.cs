namespace VendorManagement.Models.Entities
{
    public class Vendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorContactEmail { get; set; }
        public string DepartmentalOwnerEmail { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime AuditDate { get; set; }

        public int ContractCycleId { get; set; }
        public virtual ContractCycle ContractCycle { get; set; }

        public int AuditCycleId { get; set; }
        public virtual AuditCycle AuditCycle { get; set; }

        public int CriticalityLevelId { get; set; }
        public virtual CriticalityLevel CriticalityLevel { get; set; }

        public int VendorTypeId { get; set; }
        public virtual VendorType VendorType { get; set; }

        public ICollection<VendorSharedData> VendorSharedData { get; set; }
        public ICollection<Audit> Audits { get; set; }
        public ICollection<AssignedAssessment> AssignedAssessments { get; set; }
        public Vendor()
        {
            VendorSharedData = new List<VendorSharedData>();
        }
    }
}
