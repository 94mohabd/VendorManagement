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

        public DateTime AuditDueDate { get; set; }
        public DateTime ContractDueDate { get; set; }

        public string ContractStatus { get; set; }
        public string AuditStatus { get; set; }

        public int ContractCategoryStatus { get; set; }
        public int AuditCategoryStatus { get; set; }

        public Double ContractDaysUntilExpiration { get; set; }
        public Double AuditDaysUntilExpiration { get; set; }

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
