using Microsoft.EntityFrameworkCore;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class HeaderRepository
    {
        private readonly VMDbContext VMDbContext;

        public HeaderRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }

        public async Task<IEnumerable<Header>> GetAllHeadersAsync()
        {
            return await VMDbContext.Headers.ToListAsync();
        }

        public async Task<Header> GetHeaderByIdAsync(int id)
        {
            return await VMDbContext.Headers.FirstOrDefaultAsync(x => x.HeaderId == id);
        }

        public async Task AddHeaderAsync(Header header)
        {
            VMDbContext.Headers.Add(header);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task UpdateHeaderAsync(Header header)
        {
            VMDbContext.Headers.Update(header);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task DeleteHeaderAsync(int id)
        {
            var Header = await VMDbContext.Headers.FirstOrDefaultAsync(x => x.HeaderId == id);
            if (Header != null)
            {
                VMDbContext.Headers.Remove(Header);
                await VMDbContext.SaveChangesAsync();
            }
        }
    }
}
