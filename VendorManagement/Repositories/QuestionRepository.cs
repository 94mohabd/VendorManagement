using Microsoft.EntityFrameworkCore;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class QuestionRepository
    {
        private readonly VMDbContext VMDbContext;

        public QuestionRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await VMDbContext.Questions.Include(t => t.QuestionType).Include(a => a.Answers).ToListAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            return await VMDbContext.Questions.Include(t => t.QuestionType).Include(a => a.Answers).FirstOrDefaultAsync(x => x.QuestionId == id);
        }

        public async Task<Question> GetQuestionByTextAsync(string text)
        {
            return await VMDbContext.Questions.Include(x => x.Answers).FirstOrDefaultAsync(x => x.QuestionText == text);
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            VMDbContext.Questions.Add(question);
            await VMDbContext.SaveChangesAsync();
            return await GetQuestionByIdAsync(question.QuestionId);
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            VMDbContext.Questions.Update(question);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await VMDbContext.Questions.FirstOrDefaultAsync(x => x.QuestionId == id);
            if (question != null)
            {
                VMDbContext.Questions.Remove(question);
                await VMDbContext.SaveChangesAsync();
            }
        }
    }
}
