using Microsoft.EntityFrameworkCore;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class QuestionTypeRepository
    {
        private readonly VMDbContext VMDbContext;

        public QuestionTypeRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }
        public async Task<IEnumerable<QuestionType>> GetAllQuestionTypesAsync()
        {
            return await VMDbContext.QuestionTypes.ToListAsync();
        }

        public async Task<QuestionType> GetQuestionTypeById(int id)
        {
            return await VMDbContext.QuestionTypes.FirstOrDefaultAsync(x => x.QuestionTypeId == id);
        }

        public async Task CreateQuestionType(QuestionType questionType)
        {
            VMDbContext.QuestionTypes.Add(questionType);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task UpdateQuestionType(QuestionType questionType)
        {
            VMDbContext.QuestionTypes.Update(questionType);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task DeleteQuestionType(int id)
        {
            var questionType = await VMDbContext.QuestionTypes.FirstOrDefaultAsync(x => x.QuestionTypeId == id);
            if (questionType != null)
            {
                VMDbContext.QuestionTypes.Remove(questionType);
                await VMDbContext.SaveChangesAsync();
            }
        }
    }
}
