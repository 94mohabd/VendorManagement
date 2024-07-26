namespace VendorManagement.Models.Entities
{
    public class QuestionType
    {
        public int QuestionTypeId { get; set; }      
        public string Type { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<AssessmentItem> AssessmentQuestions { get; set; }
    }
}
