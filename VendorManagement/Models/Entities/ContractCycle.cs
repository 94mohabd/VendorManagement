namespace VendorManagement.Models.Entities
{
    public class ContractCycle
    {
        public int ContractCycleId { get; set; }
        public string ContractCycleType { get; set; }
        public int ContractCyclePeriod { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
