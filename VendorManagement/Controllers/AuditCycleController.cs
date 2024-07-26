using Microsoft.AspNetCore.Mvc;
using VendorManagement.Models.Entities;
using VendorManagement.Models.ViewModels;
using VendorManagement.Repositories;

namespace VendorManagement.Controllers
{
    public class AuditCycleController : Controller
    {
        private readonly AuditCycleRepository _AuditCycleRepository;

        public AuditCycleController(AuditCycleRepository AuditCycleRepository)
        {
            _AuditCycleRepository = AuditCycleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuditCycles()
        {
            var AuditCycles = await _AuditCycleRepository.GetAllAuditCyclesAsync();
            return View(AuditCycles);
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditCycleById(int id)
        {
            var AuditCycle = await _AuditCycleRepository.GetAuditCycleByIdAsync(id);
            if (AuditCycle == null)
            {
                return View();
            }
            return View(AuditCycle);
        }
        [HttpGet]
        public async Task<IActionResult> AddAuditCycle()
        {
            var viewModel = new AddAuditCycleViewModel
            {
                AuditCycles = await _AuditCycleRepository.GetAllAuditCyclesAsync()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddAuditCycle(AddAuditCycleViewModel addAuditCycle)
        {
            var auditCycle = new AuditCycle
            {
                AuditCycleType = addAuditCycle.AuditCycleType
            };

            await _AuditCycleRepository.AddAuditCycleAsync(auditCycle);
            return RedirectToAction(nameof(GetAllAuditCycles));
        }

        [HttpPost]
        public async Task<IActionResult> EditAuditCycle(int id, [FromBody] AuditCycle AuditCycle)
        {
            if (AuditCycle == null || AuditCycle.AuditCycleId != id)
            {
                return View();
            }

            var existingAuditCycle = await _AuditCycleRepository.GetAuditCycleByIdAsync(id);
            if (existingAuditCycle == null)
            {
                return View();
            }

            await _AuditCycleRepository.EditAuditCycleAsync(AuditCycle);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAuditCycle(int id)
        {
            var AuditCycle = await _AuditCycleRepository.GetAuditCycleByIdAsync(id);
            if (AuditCycle == null)
            {
                return View();
            }

            await _AuditCycleRepository.DeleteAuditCycleAsync(id);
            return View();
        }
    }
}
