using Microsoft.EntityFrameworkCore;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class AssignedAssessmentRepository
    {
        private readonly VMDbContext VMDbContext;

        public AssignedAssessmentRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }

        public async Task<IEnumerable<AssignedAssessment>> GetAllAssignedAssessments()
        {
            return await VMDbContext.AssignedAssessments.ToListAsync();
        }

        public async Task<AssignedAssessment> GetAssignedAssessmentById(int id)
        {
            return await VMDbContext.AssignedAssessments.FirstOrDefaultAsync(x => x.AssignedAssessmentId == id);
        }

        public async Task AddAssignedAssessmentAsync(AssignedAssessment assignedAssessment)
        {
            VMDbContext.AssignedAssessments.Add(assignedAssessment);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task UpdateAssignedAssessment(AssignedAssessment assignedAssessment)
        {
            VMDbContext.AssignedAssessments.Update(assignedAssessment);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task DeleteAssignedAssessment(int id)
        {
            var assignedAssessment = await VMDbContext.AssignedAssessments.FirstOrDefaultAsync(x => x.AssignedAssessmentId == id);
            if (assignedAssessment != null)
            {
                VMDbContext.AssignedAssessments.Remove(assignedAssessment);
                await VMDbContext.SaveChangesAsync();
            }
        }
    }
}
