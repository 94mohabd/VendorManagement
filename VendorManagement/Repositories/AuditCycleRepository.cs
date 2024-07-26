using Microsoft.EntityFrameworkCore;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class AuditCycleRepository
    {
        private readonly VMDbContext VMDbContext;

        public AuditCycleRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }

        public async Task<IEnumerable<AuditCycle>> GetAllAuditCyclesAsync()
        {
            return await VMDbContext.AuditCycles.ToListAsync();
        }

        public async Task<AuditCycle> GetAuditCycleByIdAsync(int id)
        {
            return await VMDbContext.AuditCycles.FirstOrDefaultAsync(x => x.AuditCycleId == id);
        }

        public async Task AddAuditCycleAsync(AuditCycle auditCycle)
        {
            VMDbContext.AuditCycles.Add(auditCycle);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task EditAuditCycleAsync(AuditCycle auditCycle)
        {
            VMDbContext.AuditCycles.Update(auditCycle);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task DeleteAuditCycleAsync(int id)
        {
            var auditCycle = await VMDbContext.AuditCycles.FirstOrDefaultAsync(x => x.AuditCycleId == id);
            if (auditCycle != null)
            {
                VMDbContext.AuditCycles.Remove(auditCycle);
                await VMDbContext.SaveChangesAsync();
            }
        }
    }
}
