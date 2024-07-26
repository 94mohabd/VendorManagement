using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using VendorManagement.Models.Entities;

namespace VendorManagement.Data
{
    public class VMDbContext : DbContext
    {
        public VMDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Assessment> Assessments { get; set; }        
        public DbSet<AssessmentAnswer> AssessmentAnswers { get; set; }       
        public DbSet<AssignedAssessment> AssignedAssessments { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<AuditCycle> AuditCycles { get; set; }
        public DbSet<ContractCycle> ContractCycles { get; set; }
        public DbSet<CriticalityLevel> CriticalityLevels { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorSharedData> VendorSharedData { get; set; }
        public DbSet<VendorType> VendorTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);

            // Seed data for CriticalityLevels
            modelBuilder.Entity<CriticalityLevel>().HasData(
                new CriticalityLevel { CriticalityLevelId = 1, Level = "Low" },
                new CriticalityLevel { CriticalityLevelId = 2, Level = "Medium" },
                new CriticalityLevel { CriticalityLevelId = 3, Level = "High" }
            );

            // Seed data for AuditCycles
            modelBuilder.Entity<AuditCycle>().HasData(
                new AuditCycle { AuditCycleId = 1, AuditCycleType = "Monthly" },
                new AuditCycle { AuditCycleId = 2, AuditCycleType = "Quarterly" },
                new AuditCycle { AuditCycleId = 3, AuditCycleType = "Semi-Annual" },
                new AuditCycle { AuditCycleId = 4, AuditCycleType = "Annual" },
                new AuditCycle { AuditCycleId = 5, AuditCycleType = "Bi-Annual" }               
            );

            // Seed data for ContractCycle
            modelBuilder.Entity<ContractCycle>().HasData(
                new ContractCycle { ContractCycleId = 1, ContractCycleType = "Monthly" },
                new ContractCycle { ContractCycleId = 2, ContractCycleType = "Quarterly" },
                new ContractCycle { ContractCycleId = 3, ContractCycleType = "Semi-Annual" },
                new ContractCycle { ContractCycleId = 4, ContractCycleType = "Annual" },
                new ContractCycle { ContractCycleId = 5, ContractCycleType = "Bi-Annual" }
            );

            // Seed data for QuestionTypes
            modelBuilder.Entity<QuestionType>().HasData(
                new QuestionType { QuestionTypeId = 1, Type = "Radio" },
                new QuestionType { QuestionTypeId = 2, Type = "Checkbox" }           
            );

            // Seed data for VendorSharedData
            modelBuilder.Entity<VendorSharedData>().HasData(
                new VendorSharedData { VendorSharedDataId = 1, SharedData = "General" },                
                new VendorSharedData { VendorSharedDataId = 2, SharedData = "Contact" },                
                new VendorSharedData { VendorSharedDataId = 3, SharedData = "Financial" },                
                new VendorSharedData { VendorSharedDataId = 4, SharedData = "ID Numbers" },                
                new VendorSharedData { VendorSharedDataId = 5, SharedData = "Web/Geolocation" },                
                new VendorSharedData { VendorSharedDataId = 6, SharedData = "Human Resources" },                
                new VendorSharedData { VendorSharedDataId = 7, SharedData = "Health/Insurance" },                
                new VendorSharedData { VendorSharedDataId = 8, SharedData = "Biometric/Genetic" },                
                new VendorSharedData { VendorSharedDataId = 9, SharedData = "Special Category" },                
                new VendorSharedData { VendorSharedDataId = 10, SharedData = "Other" }           
            );

            // Seed data for VendorTypes
            modelBuilder.Entity<VendorType>().HasData(
                new VendorType { VendorTypeId = 1, Type = "IT Hardware" },
                new VendorType { VendorTypeId = 2, Type = "Software" },
                new VendorType { VendorTypeId = 3, Type = "SaaS/PaaS" },
                new VendorType { VendorTypeId = 4, Type = "Services" },
                new VendorType { VendorTypeId = 5, Type = "Services (IT/Technical)" },
                new VendorType { VendorTypeId = 6, Type = "Physical Security" },
                new VendorType { VendorTypeId = 7, Type = "Other-Products" },
                new VendorType { VendorTypeId = 8, Type = "Other-Services" },
                new VendorType { VendorTypeId = 9, Type = "Records Information Management/Shredding" },
                new VendorType { VendorTypeId = 10, Type = "HR/Benefits" },
                new VendorType { VendorTypeId = 11, Type = "Insurance" }
            );

            modelBuilder.Entity<Header>().HasData(
                new Header { HeaderId = 1, HeaderText = "Header 1" },
                new Header { HeaderId = 2, HeaderText = "Header 2" },
                new Header { HeaderId = 3, HeaderText = "Header 3" }
             );

            modelBuilder.Entity<Question>().HasData(
                 new Question
                 {
                     QuestionId = 1,
                     QuestionText = "What is considered personally identifiable information (PII)?",
                     QuestionTypeId = 2
                 },
                 new Question
                 {
                     QuestionId = 2,
                     QuestionText = "How should sensitive data be handled in storage and transmission?",
                     QuestionTypeId = 1
                 },
                 new Question
                 {
                     QuestionId = 3,
                     QuestionText = "What steps should be taken to secure user data in a database?",
                     QuestionTypeId = 1
                 }
             );

            // Seed data for Answers
            modelBuilder.Entity<Answer>().HasData(
                new Answer { AnswerId = 1, AnswerText = "Social Security Number (SSN)", QuestionId = 1 },
                new Answer { AnswerId = 2, AnswerText = "Date of Birth", QuestionId = 1 },
                new Answer { AnswerId = 3, AnswerText = "Home Address", QuestionId = 1 },
                new Answer { AnswerId = 4, AnswerText = "Encrypted at rest and in transit", QuestionId = 2 },
                new Answer { AnswerId = 5, AnswerText = "Stored in plaintext", QuestionId = 2 },
                new Answer { AnswerId = 6, AnswerText = "Sent over unsecured connections", QuestionId = 2 },
                new Answer { AnswerId = 7, AnswerText = "Implement strong access controls", QuestionId = 3 },
                new Answer { AnswerId = 8, AnswerText = "Use weak passwords", QuestionId = 3 },
                new Answer { AnswerId = 9, AnswerText = "Share database credentials openly", QuestionId = 3 }
            );

            // Add more seed data for other entities as needed
        }
    }
}