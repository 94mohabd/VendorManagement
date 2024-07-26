using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class AnswerRepository
    {
        private readonly VMDbContext VMDbContext;

        public AnswerRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }

        public async Task<IEnumerable<Answer>> GetAllAnswersAsync()
        {
            return await VMDbContext.Answers.ToListAsync();
        }

        public async Task<Answer> GetAnswerByIdAsync(int id)
        {
            return await VMDbContext.Answers.FirstOrDefaultAsync(x => x.AnswerId == id);
        }

        public async Task CreateAnswerAsync(Answer answer)
        {
            VMDbContext.Answers.Add(answer);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task UpdateAnswerAsync(Answer answer)
        {
            VMDbContext.Answers.Update(answer);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task DeleteAnswerAsync(int id)
        {
            var answer = await VMDbContext.Answers.FirstOrDefaultAsync(x => x.AnswerId == id);
            if (answer != null)
            {
                VMDbContext.Answers.Remove(answer);
                await VMDbContext.SaveChangesAsync();
            }
        }
    }
}
