using Microsoft.EntityFrameworkCore;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class VendorSharedDataRepository
    {
        private readonly VMDbContext VMDbContext;

        public VendorSharedDataRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }

        public async Task<IEnumerable<VendorSharedData>> GetAllVendorSharedDataAsync()
        {
            return await VMDbContext.VendorSharedData.ToListAsync();
        }

        public async Task<VendorSharedData> GetVendorSharedDataByIdAsync(int id)
        {
            return await VMDbContext.VendorSharedData.FirstOrDefaultAsync(x => x.VendorSharedDataId == id);
        }

        public async Task CreateVendorSharedDataAsync(VendorSharedData vendorSharedData)
        {
            VMDbContext.VendorSharedData.Add(vendorSharedData);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task UpdateVendorSharedDataAsync(VendorSharedData vendorSharedData)
        {
            VMDbContext.VendorSharedData.Update(vendorSharedData);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task DeleteVendorSharedDataAsync(int id)
        {
            var vendorSharedData = await VMDbContext.VendorSharedData.FirstOrDefaultAsync(x => x.VendorSharedDataId == id);
            if (vendorSharedData != null)
            {
                VMDbContext.VendorSharedData.Remove(vendorSharedData);
                await VMDbContext.SaveChangesAsync();
            }
        }
    }
}
