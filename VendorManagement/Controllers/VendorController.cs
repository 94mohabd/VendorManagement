using Microsoft.AspNetCore.Mvc;
using VendorManagement.Models.Entities;
using VendorManagement.Models.ViewModels;
using VendorManagement.Repositories;

namespace VendorManagement.Controllers
{       
    public class VendorController : Controller
    {
        private readonly VendorRepository _vendorRepository;        
        private readonly ContractCycleRepository _contractCycleRepository;
        private readonly AuditCycleRepository _auditCycleRepository;
        private readonly CriticalityLevelRepository _criticalityLevelRepository;
        private readonly VendorTypeRepository _vendorTypeRepository;
        public VendorController(
           VendorRepository vendorRepository,
           ContractCycleRepository contractCycleRepository,
           AuditCycleRepository auditCycleRepository,
           CriticalityLevelRepository criticalityLevelRepository,
           VendorTypeRepository vendorTypeRepository)
        {
            _vendorRepository = vendorRepository;
            _contractCycleRepository = contractCycleRepository;
            _auditCycleRepository = auditCycleRepository;
            _criticalityLevelRepository = criticalityLevelRepository;
            _vendorTypeRepository = vendorTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVendors()
        {
            var vendors = await _vendorRepository.GetAllVendorsAsync();
            return View(vendors);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetVendorById(int id)
        {
            var vendor = await _vendorRepository.GetVendorByIdAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }
        
        [HttpGet]    
        public async Task<IActionResult> AddVendor()
        {
            var viewModel = new AddVendorViewModel
            {
                ContractCycles = await _contractCycleRepository.GetAllContractCyclesAsync(),
                AuditCycles = await _auditCycleRepository.GetAllAuditCyclesAsync(),
                CriticalityLevels = await _criticalityLevelRepository.GetAllCriticalityLevelsAsync(),
                VendorTypes = await _vendorTypeRepository.GetAllVendorTypesAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddVendor(AddVendorViewModel vendor)
        {
            var newVendor = new Vendor
            {
                VendorName = vendor.VendorName,
                VendorContactEmail = vendor.VendorContactEmail,
                DepartmentalOwnerEmail = vendor.DepartmentalOwnerEmail,
                ContractDate = vendor.ContractDate,
                AuditDate = vendor.AuditDate,
                ContractCycleId = vendor.SelectedContractCycleId,
                AuditCycleId = vendor.SelectedAuditCycleId,
                CriticalityLevelId = vendor.SelectedCriticalityLevelId,
                VendorTypeId = vendor.SelectedVendorTypeId,
            };
            await _vendorRepository.AddVendorAsync(newVendor);
            return RedirectToAction(nameof(AddVendor));
        }

        [HttpPost]
        public async Task<IActionResult> EditVendor(int id, Vendor vendor)
        {
            if (id != vendor.VendorId)
            {
                return BadRequest();
            }

            await _vendorRepository.EditVendorAsync(vendor);

            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteVendorAsync(int id)
        {
            await _vendorRepository.DeleteVendorAsync(id);

            return NoContent();
        }
    }
}
