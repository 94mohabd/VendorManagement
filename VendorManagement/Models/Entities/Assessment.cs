namespace VendorManagement.Models.Entities
{
    public class Assessment
    {
        public int AssessmentId { get; set; }
        public string Title { get; set; }
        public bool IsTemplate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<AssessmentItem> AssessmentItems { get; set; }               
        public ICollection<AssignedAssessment> AssignedAssessments { get; set; }
    }
}
