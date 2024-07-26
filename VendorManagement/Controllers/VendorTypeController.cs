using Microsoft.AspNetCore.Mvc;
using VendorManagement.Models.Entities;
using VendorManagement.Models.ViewModels;
using VendorManagement.Repositories;

namespace VendorManagement.Controllers
{
    public class VendorTypeController : Controller
    {
        private readonly VendorTypeRepository _VendorTypeRepository;

        public VendorTypeController(VendorTypeRepository VendorTypeRepository)
        {
            _VendorTypeRepository = VendorTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVendorTypes()
        {
            var VendorTypes = await _VendorTypeRepository.GetAllVendorTypesAsync();
            return View(VendorTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetVendorTypeById(int id)
        {
            var VendorType = await _VendorTypeRepository.GetVendorTypeByIdAsync(id);
            if (VendorType == null)
            {
                return View();
            }
            return View(VendorType);
        }
        [HttpGet]
        public async Task<IActionResult> AddVendorType()
        {
            var viewModel = new AddVendorTypeViewModel
            {
                VendorTypes = await _VendorTypeRepository.GetAllVendorTypesAsync()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddVendorType(AddVendorTypeViewModel addVendorType)
        {
            var VendorType = new VendorType
            {
                Type = addVendorType.Type
            };

            await _VendorTypeRepository.AddVendorTypeAsync(VendorType);
            return RedirectToAction(nameof(GetAllVendorTypes));
        }

        [HttpPost]
        public async Task<IActionResult> EditVendorType(int id, [FromBody] VendorType VendorType)
        {
            if (VendorType == null || VendorType.VendorTypeId != id)
            {
                return View();
            }

            var existingVendorType = await _VendorTypeRepository.GetVendorTypeByIdAsync(id);
            if (existingVendorType == null)
            {
                return View();
            }

            await _VendorTypeRepository.EditVendorTypeAsync(VendorType);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVendorType(int id)
        {
            var VendorType = await _VendorTypeRepository.GetVendorTypeByIdAsync(id);
            if (VendorType == null)
            {
                return View();
            }

            await _VendorTypeRepository.DeleteVendorTypeAsync(id);
            return View();
        }
    }
}
