using Microsoft.EntityFrameworkCore;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class AuditRepository
    {
        private readonly VMDbContext VMDbContext;

        public AuditRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }

        public async Task<IEnumerable<Audit>> GetAllAuditsAsync()
        {
            return await VMDbContext.Audits.ToListAsync();
        }
        public async Task<IEnumerable<Audit>> GetAllAuditsForVendorAsync(int vendorId)
        {
            return await VMDbContext.Audits
              .Include(a => a.VendorSharedData)
              .Where(a => a.VendorId == vendorId)
              .ToListAsync();
        }

        public async Task<Audit> GetAuditByIdAsync(int id)
        {
            return await VMDbContext.Audits.FirstOrDefaultAsync(x => x.AuditId == id);
        }

        public async Task AddAuditAsync(Audit audit)
        {
            VMDbContext.Audits.Add(audit);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task UpdateAuditAsync(Audit audit)
        {
            VMDbContext.Audits.Update(audit);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task DeleteAuditAsync(int id)
        {
            var audit = await VMDbContext.Audits.FirstOrDefaultAsync(x => x.AuditId == id);
            if (audit != null)
            {
                VMDbContext.Audits.Remove(audit);
                await VMDbContext.SaveChangesAsync();
            }
        }
    }
}
