using Microsoft.AspNetCore.Mvc;
using VendorManagement.Models.Entities;
using VendorManagement.Models.ViewModels;
using VendorManagement.Repositories;

namespace VendorManagement.Controllers
{
    public class CriticalityLevelController : Controller
    {
        private readonly CriticalityLevelRepository _CriticalityLevelRepository;

        public CriticalityLevelController(CriticalityLevelRepository criticalityLevelRepository)
        {
            _CriticalityLevelRepository = criticalityLevelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCriticalityLevels()
        {
            var CriticalityLevels = await _CriticalityLevelRepository.GetAllCriticalityLevelsAsync();
            return View(CriticalityLevels);
        }

        [HttpGet]
        public async Task<IActionResult> GetCriticalityLevelById(int id)
        {
            var CriticalityLevel = await _CriticalityLevelRepository.GetCriticalityLevelByIdAsync(id);
            if (CriticalityLevel == null)
            {
                return View();
            }
            return View(CriticalityLevel);
        }
        [HttpGet]
        public async Task<IActionResult> AddCriticalityLevel()
        {
            var viewModel = new AddCriticalityLevelViewModel
            {
                CriticalityLevels = await _CriticalityLevelRepository.GetAllCriticalityLevelsAsync()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddCriticalityLevel(AddCriticalityLevelViewModel addCriticalitLevel)
        {
            var CriticalityLevel = new CriticalityLevel
            {
                Level = addCriticalitLevel.Level
            };

            await _CriticalityLevelRepository.AddCriticalityLevelAsync(CriticalityLevel);
            return RedirectToAction(nameof(GetAllCriticalityLevels));
        }

        [HttpPost]
        public async Task<IActionResult> EditCriticalityLevel(int id, [FromBody] CriticalityLevel CriticalityLevel)
        {
            if (CriticalityLevel == null || CriticalityLevel.CriticalityLevelId != id)
            {
                return View();
            }

            var existingCriticalityLevel = await _CriticalityLevelRepository.GetCriticalityLevelByIdAsync(id);
            if (existingCriticalityLevel == null)
            {
                return View();
            }

            await _CriticalityLevelRepository.EditCriticalityLevelAsync(CriticalityLevel);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCriticalityLevel(int id)
        {
            var CriticalityLevel = await _CriticalityLevelRepository.GetCriticalityLevelByIdAsync(id);
            if (CriticalityLevel == null)
            {
                return View();
            }

            await _CriticalityLevelRepository.DeleteCriticalityLevelAsync(id);
            return View();
        }
    }
}
