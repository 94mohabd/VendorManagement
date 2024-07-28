using Microsoft.EntityFrameworkCore;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class VendorRepository
    {
        private readonly VMDbContext VMDbContext;

        public VendorRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }

        public async Task<IEnumerable<Vendor>?> GetVendorsSharingDataAsync(string sharedData) 
        {
            return await VMDbContext.Vendors.Where(x => x.VendorSharedData.Any(x => x.SharedData == sharedData)).ToListAsync();
        }
        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return await this.VMDbContext.Vendors
                .Include(v => v.ContractCycle)
                .Include(v => v.AuditCycle)
                .Include(v => v.CriticalityLevel)
                .Include(v => v.VendorType)
                .Include(v => v.VendorSharedData)
                .Include(v => v.Audits)
                .Include(v => v.AssignedAssessments)
                .ToListAsync();
        }

        public async Task<Vendor?> GetVendorByIdAsync(int id)
        {
            return await this.VMDbContext.Vendors
                .Include(v => v.ContractCycle)
                .Include(v => v.AuditCycle)
                .Include(v => v.CriticalityLevel)
                .Include(v => v.VendorType)
                .Include(v => v.VendorSharedData)
                .Include(v => v.Audits)
                .Include(v => v.AssignedAssessments)
                .FirstOrDefaultAsync(v => v.VendorId == id);
        }

        public async Task<Vendor> AddVendorAsync(Vendor vendor)
        {
            this.VMDbContext.Vendors.Add(vendor);
            await this.VMDbContext.SaveChangesAsync();
            return vendor;
        }

        public async Task<Vendor?> EditVendorAsync(Vendor vendor)
        {
            var existingVendor = await this.VMDbContext.Vendors.FirstOrDefaultAsync(v => v.VendorId == vendor.VendorId);
            this.VMDbContext.Vendors.Update(vendor);
            await this.VMDbContext.SaveChangesAsync();
            return existingVendor;
        }

        public async Task<Vendor?> DeleteVendorAsync(int id)
        {
            var existingVendor = await this.VMDbContext.Vendors.FirstOrDefaultAsync(x => x.VendorId == id);
            if (existingVendor != null)
            {
                this.VMDbContext.Vendors.Remove(existingVendor);
                await this.VMDbContext.SaveChangesAsync();
            }
            return existingVendor;
        }

        public async Task UpdateVendorStatusesAsync()
        {
            var vendors = await GetAllVendorsAsync();

            foreach (var vendor in vendors)
            {
                //vendor.AuditDueDate = vendor.AuditDate.AddMonths(vendor.AuditCycle.AuditCyclePeriod);
                vendor.AuditStatus = GetExpirationStatus(vendor.AuditDate, vendor.AuditDueDate).status;
                vendor.AuditCategoryStatus = GetExpirationStatus(vendor.AuditDate, vendor.AuditDueDate).category;
                vendor.AuditDaysUntilExpiration = GetExpirationStatus(vendor.AuditDate, vendor.AuditDueDate).expDays;

                //vendor.ContractDueDate = vendor.ContractDate.AddMonths(vendor.ContractCycle.ContractCyclePeriod);
                vendor.ContractStatus = GetExpirationStatus(vendor.ContractDate, vendor.ContractDueDate).status;
                vendor.ContractCategoryStatus = GetExpirationStatus(vendor.ContractDate, vendor.ContractDueDate).category;
                vendor.ContractDaysUntilExpiration = GetExpirationStatus(vendor.ContractDate, vendor.ContractDueDate).expDays;

                await EditVendorAsync(vendor);
            }
        }

        private (string status, int category, Double expDays) GetExpirationStatus(DateTime currentDate, DateTime dueDate)
        {
            var daysUntilExpiration = (dueDate - currentDate).TotalDays;

            if (daysUntilExpiration < 0)
            {
                return ("Overdue", 0, daysUntilExpiration);
            }
            else if (daysUntilExpiration <= 30)
            {
                return ("Expires in less than 30 days", 30, daysUntilExpiration);
            }
            else if (daysUntilExpiration <= 60)
            {
                return ("Expires in less than 60 days", 60, daysUntilExpiration);
            }
            else
            {
                return ("Valid", 1, daysUntilExpiration);
            }
        }
    }
}
