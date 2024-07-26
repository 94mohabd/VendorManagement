using VendorManagement.Models.Entities;

namespace VendorManagement.Models.ViewModels
{
    public class AddContractCycleViewModel
    {
        public int ContractCycleId { get; set; }
        public string ContractCycleType { get; set; }                   
        public IEnumerable<ContractCycle> ContractCycles { get; set; }
    }
}
