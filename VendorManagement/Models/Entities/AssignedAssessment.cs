namespace VendorManagement.Models.Entities
{
    public class AssignedAssessment
    {
        public int AssignedAssessmentId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public string Status { get; set; }

        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        public int AssessmentId { get; set; }
        public virtual Assessment Assessment { get; set; }
    }
}
