namespace VendorManagement.Models.Entities
{
    public class AssessmentAnswer
    {
        public int AssessmentAnswerId { get; set; }                
        public int AssessmentQuestionId { get; set; }
        public AssessmentItem AssessmentQuestion { get; set; }
        public string AnswerText { get; set; }      
    }
}
