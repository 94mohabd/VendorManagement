using Microsoft.EntityFrameworkCore;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class CriticalityLevelRepository
    {
        private readonly VMDbContext VMDbContext;

        public CriticalityLevelRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }

        public async Task<IEnumerable<CriticalityLevel>> GetAllCriticalityLevelsAsync()
        {
            return await VMDbContext.CriticalityLevels.ToListAsync();
        }

        public async Task<CriticalityLevel> GetCriticalityLevelByIdAsync(int id)
        {
            return await VMDbContext.CriticalityLevels.FirstOrDefaultAsync(x => x.CriticalityLevelId == id);
        }

        public async Task AddCriticalityLevelAsync(CriticalityLevel criticalityLevel)
        {
            VMDbContext.CriticalityLevels.Add(criticalityLevel);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task EditCriticalityLevelAsync(CriticalityLevel criticalityLevel)
        {
            VMDbContext.CriticalityLevels.Update(criticalityLevel);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task DeleteCriticalityLevelAsync(int id)
        {
            var criticalityLevel = await VMDbContext.CriticalityLevels.FirstOrDefaultAsync(x => x.CriticalityLevelId == id);
            if (criticalityLevel != null)
            {
                VMDbContext.CriticalityLevels.Remove(criticalityLevel);
                await VMDbContext.SaveChangesAsync();
            }
        }
    }
}
