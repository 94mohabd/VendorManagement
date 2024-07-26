using VendorManagement.Models.Entities;

namespace VendorManagement.Models.ViewModels
{
    public class AssessmentViewModel
    {
        public int AssessmentId { get; set; }
        public string Title { get; set; }
        public bool IsTemplate { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Header> Headers { get; set; }
    }
}
