using Microsoft.AspNetCore.Mvc;
using VendorManagement.Models.Entities;
using VendorManagement.Models.ViewModels;
using VendorManagement.Repositories;

namespace VendorManagement.Controllers
{    
    public class ContractCycleController : Controller
    {
        private readonly ContractCycleRepository _contractCycleRepository;

        public ContractCycleController(ContractCycleRepository contractCycleRepository)
        {
            _contractCycleRepository = contractCycleRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllContractCycles()
        {
            var contractCycles = await _contractCycleRepository.GetAllContractCyclesAsync();
            return View(contractCycles);
        }
       
        [HttpGet]
        public async Task<IActionResult> GetContractCycleById(int id)
        {
            var contractCycle = await _contractCycleRepository.GetContractCycleByIdAsync(id);
            if (contractCycle == null)
            {
                return View();
            }
            return View(contractCycle);
        }
        [HttpGet]
        public async Task<IActionResult> AddContractCycle()
        {
            var viewModel = new AddContractCycleViewModel
            {
                ContractCycles = await _contractCycleRepository.GetAllContractCyclesAsync()
            };           

            return View(viewModel);           
        }
        [HttpPost]
        public async Task<IActionResult> AddContractCycle(AddContractCycleViewModel addContractCycle)
        {
            if (ModelState.IsValid)
            {
                var contracCycle = new ContractCycle
                {
                    ContractCycleType = addContractCycle.ContractCycleType                    
                };

                await _contractCycleRepository.AddContractCycleAsync(contracCycle);
                return RedirectToAction(nameof(GetAllContractCycles));
            }

            return View(addContractCycle);
        }
       
        [HttpPost]
        public async Task<IActionResult> EditContractCycle(int id, [FromBody] ContractCycle contractCycle)
        {
            if (contractCycle == null || contractCycle.ContractCycleId != id)
            {
                return View();
            }

            var existingContractCycle = await _contractCycleRepository.GetContractCycleByIdAsync(id);
            if (existingContractCycle == null)
            {
                return View();
            }

            await _contractCycleRepository.EditContractCycleAsync(contractCycle);
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> DeleteContractCycle(int id)
        {
            var contractCycle = await _contractCycleRepository.GetContractCycleByIdAsync(id);
            if (contractCycle == null)
            {
                return View();
            }

            await _contractCycleRepository.DeleteContractCycleAsync(id);
            return View();
        }
    }
}
