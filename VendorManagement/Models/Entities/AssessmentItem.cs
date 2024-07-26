namespace VendorManagement.Models.Entities
{
    public class AssessmentItem
    {
        public int AssessmentItemId { get; set; }
        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
        public int Order { get; set; }        
        public string ItemType { get; set; }
        public int? QuestionTypeId { get; set; }
        public virtual QuestionType? QuestionType { get; set; }
        public string ItemText { get; set; }
        public ICollection<AssessmentAnswer>? AssessmentAnswers { get; set; }
    }
}
