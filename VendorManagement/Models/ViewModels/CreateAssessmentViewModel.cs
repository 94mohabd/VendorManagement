using VendorManagement.Models.Entities;

namespace VendorManagement.Models.ViewModels
{
    public class CreateAssessmentViewModel
    {        
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Header> Headers { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<QuestionType> QuestionTypes { get; set; }
    }
    public class AddAssessmentRequest
    {
        public string Title { get; set; }
        public List<AssessmentItemRequest> Items { get; set; }
    }

    public class AssessmentItemRequest
    {
        public int Id { get; set; }
        public string Type { get; set; } 
        public int QuestionTypeId { get; set; } 
        public string Text { get; set; }
    }
    public class EditAssessmentRequest
    {
        public int AssessmentId { get; set; }
        public string Title { get; set; }
        public List<AssessmentItemRequest> Items { get; set; }
    }
    public class AddQuestionRequest
    {
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public List<string> Answers { get; set; }
    }
    public class QuestionResponse
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionType { get; set; }
        public List<AnswerResponse> Answers { get; set; }
    }

    public class AnswerResponse
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
    }
}
