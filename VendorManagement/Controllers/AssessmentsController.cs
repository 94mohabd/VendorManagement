using Azure.Core;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using VendorManagement.Models.Entities;
using VendorManagement.Models.ViewModels;
using VendorManagement.Repositories;

namespace VendorManagement.Controllers
{
    public class AssessmentsController : Controller
    {
        private readonly AssessmentRepository assessmentRepository;
        private readonly QuestionRepository questionRepository;
        private readonly HeaderRepository headerRepository;
        private readonly AnswerRepository answerRepository;
        private readonly QuestionTypeRepository questionTypeRepository;
        private readonly AssignedAssessmentRepository assignedAssessmentRepository;
        private readonly VendorRepository vendorRepository;

        public AssessmentsController(AssessmentRepository assessmentRepository,
            QuestionRepository questionRepository,
            HeaderRepository headerRepository,
            AnswerRepository answerRepository,
            QuestionTypeRepository questionTypeRepository,
            AssignedAssessmentRepository assignedAssessmentRepository,
            VendorRepository vendorRepository)
        {
            this.assessmentRepository = assessmentRepository;
            this.questionRepository = questionRepository;
            this.headerRepository = headerRepository;
            this.answerRepository = answerRepository;
            this.questionTypeRepository = questionTypeRepository;
            this.assignedAssessmentRepository = assignedAssessmentRepository;
            this.vendorRepository = vendorRepository;
        }
        public async Task<IActionResult> Index()
        {
            var assessments = await assessmentRepository.GetAllAssessmentsAsync();
            return View(assessments);
        }
        public async Task<IActionResult> CreateAssessment()
        {
            var model = new CreateAssessmentViewModel
            {
                Questions = await questionRepository.GetAllQuestionsAsync(),
                Headers = await headerRepository.GetAllHeadersAsync(),
                Answers = await answerRepository.GetAllAnswersAsync(),
                QuestionTypes = await questionTypeRepository.GetAllQuestionTypesAsync()
            };
            return View(model);
        }
        public async Task<IActionResult> EditAssessment(int assessmentId)
        {
            var assessment = await assessmentRepository.GetAssessmentByIdAsync(assessmentId);

            if (assessment != null)
            {
                var allQuestions = await questionRepository.GetAllQuestionsAsync();
                var allHeaders = await headerRepository.GetAllHeadersAsync();

                // Get IDs of questions and headers that are already used in the assessment
                var usedQuestionIds = assessment.AssessmentItems.Where(x => x.ItemType == "question").Select(ai => ai.ItemText).ToHashSet();
                var usedHeaderIds = assessment.AssessmentItems.Where(x => x.ItemType == "header").Select(ai => ai.ItemText).ToHashSet();

                // Filter questions and headers that are not already used in the assessment
                var unusedQuestions = allQuestions.Where(q => !usedQuestionIds.Contains(q.QuestionText)).ToList();
                var unusedHeaders = allHeaders.Where(h => !usedHeaderIds.Contains(h.HeaderText)).ToList();

                var viewModel = new EditAssessmentViewModel
                {
                    Questions = unusedQuestions,
                    Headers = unusedHeaders,
                    Answers = await answerRepository.GetAllAnswersAsync(),
                    QuestionTypes = await questionTypeRepository.GetAllQuestionTypesAsync(),
                    Vendors = await vendorRepository.GetAllVendorsAsync(),
                    AssignedAssessmentCount = assessment.AssignedAssessments.Count,
                    AssessmentId = assessmentId,
                    Assessment = assessment,
                    IsTemplate = assessment.IsTemplate,
                    Title = assessment.Title,
                    CreateDate = assessment.CreateDate,
                    OrderedItems = assessment.AssessmentItems // Set the ordered items
                };

                return View(viewModel);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditAssessment([FromBody] EditAssessmentRequest request)
        {
            if (string.IsNullOrEmpty(request.Title) || request.Items == null || !request.Items.Any())
            {
                return Json(new { success = false, errors = new[] { "Invalid assessment data. Please provide a title and select at least one item." } });
            }

            try
            {
                // Retrieve the existing assessment from the database
                var existingAssessment = await assessmentRepository.GetAssessmentByIdAsync(request.AssessmentId);
                if (existingAssessment == null)
                {
                    return Json(new { success = false, errors = new[] { "Assessment not found." } });
                }
                var tempAssessmentItems = new List<AssessmentItem>(existingAssessment.AssessmentItems);

                var existingAssessmentItemIds = existingAssessment.AssessmentItems.ToList().Select(x => x.AssessmentItemId);
                // Update the assessment properties
                existingAssessment.Title = request.Title;
                existingAssessment.ModifiedDate = DateTime.Now;

                existingAssessment.AssessmentItems.Clear();
                // Add or update assessment items based on the request
                var order = 1;
                foreach (var item in request.Items)
                {
                    if (item.Type == "question")
                    {
                        var question = await questionRepository.GetQuestionByIdAsync(item.Id);
                        if (question != null)
                        {
                            // Create a new AssessmentQuestion
                            var assessmentQuestion = new AssessmentItem
                            {
                                ItemType = item.Type,
                                QuestionTypeId = question.QuestionTypeId,
                                QuestionType = question.QuestionType,
                                ItemText = item.Text,
                                Order = order,
                                AssessmentAnswers = question.Answers
                                    .Select(answer => new AssessmentAnswer
                                    {
                                        AnswerText = answer.AnswerText
                                    })
                                    .ToList()
                            };

                            // Add the assessment question to the existing assessment
                            existingAssessment.AssessmentItems.Add(assessmentQuestion);
                            order++;
                        }
                        else
                        {
                            var itemId = existingAssessmentItemIds.FirstOrDefault(id => id == item.Id);
                            if (itemId > 0)
                            {
                                var existingItem = tempAssessmentItems.FirstOrDefault(x => x.AssessmentItemId == itemId);
                                var assessmentQuestion = new AssessmentItem
                                {
                                    ItemType = existingItem.ItemType,
                                    QuestionTypeId = existingItem.QuestionTypeId,
                                    QuestionType = existingItem.QuestionType,
                                    ItemText = existingItem.ItemText,
                                    Order = order,
                                    AssessmentAnswers = existingItem.AssessmentAnswers
                                    .Select(answer => new AssessmentAnswer
                                    {
                                        AnswerText = answer.AnswerText
                                    })
                                    .ToList()
                                };

                                // Add the assessment question to the existing assessment
                                existingAssessment.AssessmentItems.Add(assessmentQuestion);
                                order++;
                            }
                            else
                            {
                                return Json(new { success = false, errors = new[] { $"Question with ID {item.Id} not found." } });
                            }
                        }
                    }
                    else if (item.Type == "header")
                    {
                        var header = await headerRepository.GetHeaderByIdAsync(item.Id);
                        if (header != null)
                        {
                            // Create a new AssessmentHeader
                            var assessmentHeader = new AssessmentItem
                            {
                                ItemType = item.Type,                                
                                ItemText = item.Text,
                                Order = order
                            };

                            // Add the assessment header to the existing assessment
                            existingAssessment.AssessmentItems.Add(assessmentHeader);
                            order++;
                        }
                        else
                        {
                            var itemId = existingAssessmentItemIds.FirstOrDefault(id => id == item.Id);
                            if (itemId > 0)
                            {
                                var existingItem = tempAssessmentItems.FirstOrDefault(x => x.AssessmentItemId == itemId);
                                var assessmentHeader = new AssessmentItem
                                {
                                    ItemType = item.Type,                                    
                                    ItemText = item.Text,
                                    Order = order
                                };

                                // Add the assessment question to the existing assessment
                                existingAssessment.AssessmentItems.Add(assessmentHeader);
                                order++;
                            }
                            else
                            {
                                return Json(new { success = false, errors = new[] { $"Question with ID {item.Id} not found." } });
                            }
                        }                    
                    }
                }

                // Update the existing assessment in the repository
                await assessmentRepository.UpdateAssessmentAsync(existingAssessment);
                var selectedAssessmentId = existingAssessment.AssessmentId;
                // Return a success response
                return Json(new { success = true, selectedAssessmentId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { ex.Message } });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddHeader([FromBody] Header newHeader)
        {
            if (string.IsNullOrEmpty(newHeader.HeaderText))
            {
                return Json(new { success = false, errors = new[] { "Header text cannot be empty." } });
            }

            var header = new Header
            {
                HeaderText = newHeader.HeaderText
            };

            await headerRepository.AddHeaderAsync(header);

            return Json(new { success = true, header });
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] AddQuestionRequest request)
        {
            if (string.IsNullOrEmpty(request.QuestionText))
            {
                return Json(new { success = false, errors = new[] { "Question text cannot be empty." } });
            }

            var question = new Question
            {
                QuestionTypeId = request.QuestionTypeId,
                QuestionText = request.QuestionText,
                Answers = request.Answers.Select(answer => new Answer
                {
                    AnswerText = answer
                }).ToList()
            };

            var addedQuestion = await questionRepository.AddQuestionAsync(question);
            var questionResponse = new QuestionResponse
            {
                QuestionId = addedQuestion.QuestionId,
                QuestionText = addedQuestion.QuestionText,
                QuestionTypeId = addedQuestion.QuestionType.QuestionTypeId,
                QuestionType = addedQuestion.QuestionType.Type,
                Answers = addedQuestion.Answers.Select(a => new AnswerResponse
                {
                    AnswerId = a.AnswerId,
                    AnswerText = a.AnswerText
                }).ToList()
            };

            return Json(new { success = true, questionResponse });
        }
        [HttpPost]
        public async Task<IActionResult> AddAssessment([FromBody] AddAssessmentRequest request)
        {
            if (string.IsNullOrEmpty(request.Title) || request.Items == null || !request.Items.Any())
            {
                return Json(new { success = false, errors = new[] { "Invalid assessment data. Please provide a title and select at least one item." } });
            }

            try
            {
                var assessment = new Assessment
                {
                    Title = request.Title,
                    CreateDate = DateTime.Now,
                    IsTemplate = true,
                    AssessmentItems = new List<AssessmentItem>() // Initialize the AssessmentItems collection
                };

                foreach (var item in request.Items)
                {
                    if (item.Type == "question")
                    {
                        var question = await questionRepository.GetQuestionByIdAsync(item.Id);
                        if (question != null)
                        {
                            // Create a new AssessmentQuestion
                            var assessmentQuestion = new AssessmentItem
                            {
                                ItemType = item.Type,
                                QuestionTypeId = question.QuestionTypeId,
                                QuestionType = question.QuestionType,
                                ItemText = item.Text,
                                AssessmentAnswers = question.Answers
                                    .Select(answer => new AssessmentAnswer
                                    {
                                        AnswerText = answer.AnswerText
                                    })
                                    .ToList()
                            };

                            // Add the assessment question to the assessment
                            assessment.AssessmentItems.Add(assessmentQuestion);
                        }
                        else
                        {
                            // Handle case where question with specified ID is not found
                            return Json(new { success = false, errors = new[] { $"Question with ID {item.Id} not found." } });
                        }
                    }
                    else if (item.Type == "header")
                    {
                        // Create a new AssessmentHeader
                        var assessmentHeader = new AssessmentItem
                        {
                            ItemType = item.Type,
                            ItemText = item.Text
                        };

                        // Add the assessment header to the assessment
                        assessment.AssessmentItems.Add(assessmentHeader);
                    }
                    else
                    {
                        return Json(new { success = false, errors = new[] { $"Invalid item type: {item.Type}" } });
                    }
                }

                // Add the assessment to the repository
                var selectedAssessmentId = await assessmentRepository.AddAssessmentAsync(assessment);

                // Return a success response with the new assessment ID
                return Json(new { success = true, selectedAssessmentId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { ex.Message } });
            }
        }

        public async Task<IActionResult> AssessmentDetails(int assessmentId)
        {
            var assessment = await assessmentRepository.GetAssessmentByIdAsync(assessmentId);

            if (assessment == null)
            {
                return RedirectToAction("Index");
            }
            var viewModel = new AssessmentDetailsViewModel
            {
                Vendors = await vendorRepository.GetAllVendorsAsync(),
                AssignedAssessmentCount = assessment.AssignedAssessments.Select(aa => aa.Vendor).Distinct().Count(),
                AssignedVendors = assessment.AssignedAssessments.Select(aa => aa.Vendor).Distinct().ToList(),
                AssessmentId = assessmentId,
                Assessment = assessment,
                IsTemplate = assessment.IsTemplate,
                Title = assessment.Title,
                CreateDate = assessment.CreateDate,
                OrderedItems = assessment.AssessmentItems // Set the ordered items
            };

            return View(viewModel);

        }
        [HttpPost]
        public async Task<IActionResult> AssignAssessment(AssessmentDetailsViewModel model)
        {
            var selectedAssessment = await assessmentRepository.GetAssessmentByIdAsync(model.AssessmentId);
            if (selectedAssessment != null)
            {
                var assignedAssessment = new AssignedAssessment
                {
                    AssignmentDate = DateTime.Now,
                    Status = "Assigned",
                    AssessmentId = selectedAssessment.AssessmentId,
                    VendorId = model.SelectedVendorId
                };

                await assignedAssessmentRepository.AddAssignedAssessmentAsync(assignedAssessment);
            }

            return RedirectToAction("VendorDetails", "VendorManagement", new { vendorId = model.SelectedVendorId });
        }
    }
}
