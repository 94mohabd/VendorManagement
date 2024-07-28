using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendorManagement.Models.Entities;
using VendorManagement.Models.ViewModels;
using VendorManagement.Repositories;

namespace VendorManagement.Controllers
{
    public class VendorManagementController : Controller
    {
        private readonly VendorRepository vendorRepository;
        private readonly ContractCycleRepository contractCycleRepository;
        private readonly AuditCycleRepository auditCycleRepository;
        private readonly CriticalityLevelRepository criticalityLevelRepository;
        private readonly VendorTypeRepository vendorTypeRepository;
        private readonly VendorSharedDataRepository vendorSharedDataRepository;
        private readonly AuditRepository auditRepository;
        private readonly AssessmentRepository assessmentRepository;
        private readonly AssignedAssessmentRepository assignedAssessmentRepository;

        public VendorManagementController(VendorRepository vendorRepository,
           ContractCycleRepository contractCycleRepository,
           AuditCycleRepository auditCycleRepository,
           CriticalityLevelRepository criticalityLevelRepository,
           VendorTypeRepository vendorTypeRepository,
           VendorSharedDataRepository vendorSharedDataRepository,
           AuditRepository auditRepository,
           AssessmentRepository assessmentRepository,
           AssignedAssessmentRepository assignedAssessmentRepository)
        {
            this.vendorRepository = vendorRepository;
            this.contractCycleRepository = contractCycleRepository;
            this.auditCycleRepository = auditCycleRepository;
            this.criticalityLevelRepository = criticalityLevelRepository;
            this.vendorTypeRepository = vendorTypeRepository;
            this.vendorSharedDataRepository = vendorSharedDataRepository;
            this.auditRepository = auditRepository;
            this.assessmentRepository = assessmentRepository;
            this.assignedAssessmentRepository = assignedAssessmentRepository;
        }        
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.NotificationContactInformation = await vendorRepository.GetVendorsSharingDataAsync("Contact Information");
            ViewBag.NotificationGeneralInformation = await vendorRepository.GetVendorsSharingDataAsync("General Information");
            ViewBag.NotificationFinancialData = await vendorRepository.GetVendorsSharingDataAsync("Financial Data");
            ViewBag.NotificationIdentificationNumbers = await vendorRepository.GetVendorsSharingDataAsync("ID Numbers");
            ViewBag.NotificationWebBrowsingData = await vendorRepository.GetVendorsSharingDataAsync("Web Browsing");
            ViewBag.NotificationHumanResources = await vendorRepository.GetVendorsSharingDataAsync("Human Resources");
            ViewBag.NotificationMedicalHealth = await vendorRepository.GetVendorsSharingDataAsync("Medical/Insurance");
            ViewBag.NotificationBiometricGenetic = await vendorRepository.GetVendorsSharingDataAsync("Biometric/Genetic");
            ViewBag.NotificationSpecialCategories = await vendorRepository.GetVendorsSharingDataAsync("Special Category");
            ViewBag.NotificationOther = await vendorRepository.GetVendorsSharingDataAsync("Other");
           
            return View();
        }
        public async Task<IActionResult> Vendors()
        {            
            await this.vendorRepository.UpdateVendorStatusesAsync();
            var viewModel = new AddVendorViewModel
            {
                ContractCycles = await contractCycleRepository.GetAllContractCyclesAsync(),
                AuditCycles = await auditCycleRepository.GetAllAuditCyclesAsync(),
                CriticalityLevels = await criticalityLevelRepository.GetAllCriticalityLevelsAsync(),
                VendorTypes = await vendorTypeRepository.GetAllVendorTypesAsync(),
                VendorSharedData = await vendorSharedDataRepository.GetAllVendorSharedDataAsync(),
                Vendors = await vendorRepository.GetAllVendorsAsync()
            };            
            return View(viewModel);            
        }
        [HttpPost]
        public async Task<IActionResult> Vendors(AddVendorViewModel model)
        {
            await this.vendorRepository.UpdateVendorStatusesAsync();                        
                                                          
            var newVendor = new Vendor
            {
                VendorName = model.VendorName,
                VendorContactEmail = model.VendorContactEmail,
                DepartmentalOwnerEmail = model.DepartmentalOwnerEmail,
                ContractDate = model.ContractDate,
                ContractStatus = "check",
                AuditStatus = "check",
                ContractCycle = await contractCycleRepository.GetContractCycleByIdAsync(model.SelectedContractCycleId),
                AuditDate = model.AuditDate,
                AuditCycle = await auditCycleRepository.GetAuditCycleByIdAsync(model.SelectedAuditCycleId),                               
                CriticalityLevel = await criticalityLevelRepository.GetCriticalityLevelByIdAsync(model.SelectedCriticalityLevelId),
                VendorType = await vendorTypeRepository.GetVendorTypeByIdAsync(model.SelectedVendorTypeId),
            };

            // Handle multi-select values for VendorSharedData
            foreach (var sharedDataId in model.SelectedVendorSharedDataIds)
            {
                var sharedData = await vendorSharedDataRepository.GetVendorSharedDataByIdAsync(sharedDataId);
                if (sharedData != null)
                {
                    newVendor.VendorSharedData.Add(sharedData);
                }
            }

            // Save the new vendor to the database
            await vendorRepository.AddVendorAsync(newVendor);
            var viewModel = new AddVendorViewModel
            {
                ContractCycles = await contractCycleRepository.GetAllContractCyclesAsync(),
                AuditCycles = await auditCycleRepository.GetAllAuditCyclesAsync(),
                CriticalityLevels = await criticalityLevelRepository.GetAllCriticalityLevelsAsync(),
                VendorTypes = await vendorTypeRepository.GetAllVendorTypesAsync(),
                VendorSharedData = await vendorSharedDataRepository.GetAllVendorSharedDataAsync(),
                Vendors = await vendorRepository.GetAllVendorsAsync()
            };
            return RedirectToAction("Vendors");            
        }
        public async Task<IActionResult> VendorDetails(int vendorId)
        {
            await this.vendorRepository.UpdateVendorStatusesAsync();
            var vendor = await vendorRepository.GetVendorByIdAsync(vendorId);

            if (vendor != null)
            {
                var viewModel = new VendorDetailsViewModel
                {
                    Vendor = vendor,
                    Audits = await auditRepository.GetAllAuditsForVendorAsync(vendorId),
                    Assessments = await assessmentRepository.GetAllAssessmentsAsync(),
                    AssignedAssessments = vendor.AssignedAssessments.ToList(),
                    VendorSharedData = await vendorSharedDataRepository.GetAllVendorSharedDataAsync(),
                    VendorName = vendor.VendorName,
                    VendorContactEmail = vendor.VendorContactEmail,
                    DepartmentalOwnerEmail = vendor.DepartmentalOwnerEmail,
                    AuditDate = vendor.AuditDate,
                    ContractDate = vendor.ContractDate,
                    SelectedVendorTypeId = vendor.VendorTypeId,
                    SelectedAuditCycleId = vendor.AuditCycleId,
                    SelectedContractCycleId = vendor.ContractCycleId,
                    SelectedCriticalityLevelId = vendor.CriticalityLevelId,
                    ContractCycles = await contractCycleRepository.GetAllContractCyclesAsync(),
                    AuditCycles = await auditCycleRepository.GetAllAuditCyclesAsync(),
                    CriticalityLevels = await criticalityLevelRepository.GetAllCriticalityLevelsAsync(),
                    VendorTypes = await vendorTypeRepository.GetAllVendorTypesAsync(),

                };                
                return View(viewModel);
            }
            return RedirectToAction("Vendors");
        }
        [HttpPost]
        public async Task<IActionResult> EditVendor(VendorDetailsViewModel model)
        {
            var vendor = await vendorRepository.GetVendorByIdAsync(model.VendorId);
            if (vendor != null) 
            {
                vendor.VendorName = model.VendorName;
                vendor.DepartmentalOwnerEmail = model.DepartmentalOwnerEmail;
                vendor.VendorContactEmail = model.VendorContactEmail;                
                vendor.VendorTypeId = model.SelectedVendorTypeId;                
                vendor.ContractDate = model.ContractDate;
                vendor.ContractCycleId = model.SelectedContractCycleId;
                vendor.AuditCycleId = model.SelectedAuditCycleId;
                await vendorRepository.EditVendorAsync(vendor);
            }
            return RedirectToAction("VendorDetails", "VendorManagement", new { vendorId = model.VendorId });
        }
        [HttpPost]
        public async Task<IActionResult> AddAudit(VendorDetailsViewModel model)
        {
            var selectedVendor = await vendorRepository.GetVendorByIdAsync(model.SelectedVendorId);
            if (selectedVendor != null)
            {
                selectedVendor.VendorSharedData = new List<VendorSharedData>();
                selectedVendor.AuditDate = DateTime.Now;
                var newAudit = new Audit
                {
                    VendorId = model.SelectedVendorId,
                    AuditDate = DateTime.Now
                };
                foreach (var sharedDataId in model.SelectedVendorSharedDataIds)
                {
                    var sharedData = await vendorSharedDataRepository.GetVendorSharedDataByIdAsync(sharedDataId);
                    if (sharedData != null)
                    {
                        newAudit.VendorSharedData.Add(sharedData);
                        selectedVendor.VendorSharedData.Add(sharedData);
                    }
                }
                await auditRepository.AddAuditAsync(newAudit);
                await vendorRepository.EditVendorAsync(selectedVendor);                                
            }

            return RedirectToAction("VendorDetails", "VendorManagement", new { vendorId = model.SelectedVendorId });
        }
        [HttpPost]
        public async Task<IActionResult> AssignAssessment(VendorDetailsViewModel model)
        {
            var selectedVendor = await vendorRepository.GetVendorByIdAsync(model.SelectedVendorId);
            if (selectedVendor != null)
            {
                var assignedAssessment = new AssignedAssessment
                {
                    AssignmentDate = DateTime.Now,
                    Status = "Assigned",
                    AssessmentId = model.SelectedAssessmentId,
                    VendorId = selectedVendor.VendorId
                };

                await assignedAssessmentRepository.AddAssignedAssessmentAsync(assignedAssessment);
            }

            return RedirectToAction("VendorDetails", "VendorManagement", new { vendorId = model.SelectedVendorId });
        }        
    }
}
