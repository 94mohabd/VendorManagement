using VendorManagement.Models.Entities;

namespace VendorManagement.Models.ViewModels
{
    public class EditAssessmentViewModel
    {
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Header> Headers { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<QuestionType> QuestionTypes { get; set; }
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

    public class AssessmentItemViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; } // "header" or "question"
        public int Order { get; set; }
        public List<AnswerViewModel> Answers { get; set; } // Only for questions
        public string QuestionType { get; set; } // Only for questions
    }
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
    }
}
