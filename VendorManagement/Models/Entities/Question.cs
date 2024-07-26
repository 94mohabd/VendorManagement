namespace VendorManagement.Models.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public virtual QuestionType QuestionType { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }     
    }
}
