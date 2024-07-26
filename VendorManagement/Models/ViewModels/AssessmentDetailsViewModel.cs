using VendorManagement.Models.Entities;

namespace VendorManagement.Models.ViewModels
{
    public class AssessmentDetailsViewModel
    {
        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
        public int AssignedAssessmentCount { get; set; }
        public string Title { get; set; }
        public bool IsTemplate { get; set; }
        public DateTime CreateDate { get; set; }
        public int SelectedVendorId { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
        public IEnumerable<AssessmentItem> OrderedItems { get; set; } // New property

    }

}
