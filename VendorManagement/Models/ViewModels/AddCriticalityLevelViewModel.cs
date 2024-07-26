using VendorManagement.Models.Entities;

namespace VendorManagement.Models.ViewModels
{
    public class AddCriticalityLevelViewModel
    {
        public int CriticalityLevelId { get; set; }
        public string Level { get; set; } // e.g., Low, Medium, High       
        public IEnumerable<CriticalityLevel> CriticalityLevels { get; set; }
    }
}
