using Microsoft.EntityFrameworkCore;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class VendorTypeRepository
    {
        private readonly VMDbContext VMDbContext;

        public VendorTypeRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }

        public async Task<IEnumerable<VendorType>> GetAllVendorTypesAsync()
        {
            return await VMDbContext.VendorTypes.ToListAsync();
        }

        public async Task<VendorType> GetVendorTypeByIdAsync(int id)
        {
            return await VMDbContext.VendorTypes.FirstOrDefaultAsync(x => x.VendorTypeId == id);
        }

        public async Task AddVendorTypeAsync(VendorType vendorType)
        {
            VMDbContext.VendorTypes.Add(vendorType);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task EditVendorTypeAsync(VendorType vendorType)
        {
            VMDbContext.VendorTypes.Update(vendorType);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task DeleteVendorTypeAsync(int id)
        {
            var vendorType = await VMDbContext.VendorTypes.FirstOrDefaultAsync(x => x.VendorTypeId == id);
            if (vendorType != null)
            {
                VMDbContext.VendorTypes.Remove(vendorType);
                await VMDbContext.SaveChangesAsync();
            }
        }
    }
}
