using Microsoft.EntityFrameworkCore;
using VendorManagement.Data;
using VendorManagement.Models.Entities;

namespace VendorManagement.Repositories
{
    public class ContractCycleRepository
    {
        private readonly VMDbContext VMDbContext;

        public ContractCycleRepository(VMDbContext VMDbContext)
        {
            this.VMDbContext = VMDbContext;
        }

        public async Task<IEnumerable<ContractCycle>> GetAllContractCyclesAsync()
        {
            return await VMDbContext.ContractCycles.ToListAsync();
        }

        public async Task<ContractCycle> GetContractCycleByIdAsync(int id)
        {
            return await VMDbContext.ContractCycles.FirstOrDefaultAsync(x => x.ContractCycleId == id);
        }

        public async Task AddContractCycleAsync(ContractCycle contractCycle)
        {
            VMDbContext.ContractCycles.Add(contractCycle);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task EditContractCycleAsync(ContractCycle contractCycle)
        {
            VMDbContext.ContractCycles.Update(contractCycle);
            await VMDbContext.SaveChangesAsync();
        }

        public async Task DeleteContractCycleAsync(int id)
        {
            var contractCycle = await VMDbContext.ContractCycles.FirstOrDefaultAsync(x => x.ContractCycleId == id);
            if (contractCycle != null)
            {
                VMDbContext.ContractCycles.Remove(contractCycle);
                await VMDbContext.SaveChangesAsync();
            }
        }
    }
}
