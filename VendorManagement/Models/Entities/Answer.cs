namespace VendorManagement.Models.Entities
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
