using VendorManagement.Data;
using Microsoft.EntityFrameworkCore;
using VendorManagement.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<VMDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("VMDbConnectionString")));

// Register repositories
builder.Services.AddScoped<AnswerRepository>();
builder.Services.AddScoped<AssessmentRepository>();
builder.Services.AddScoped<AssignedAssessmentRepository>();
builder.Services.AddScoped<AuditCycleRepository>();
builder.Services.AddScoped<AuditRepository>();
builder.Services.AddScoped<ContractCycleRepository>();
builder.Services.AddScoped<CriticalityLevelRepository>();
builder.Services.AddScoped<QuestionRepository>();
builder.Services.AddScoped<VendorRepository>();
builder.Services.AddScoped<VendorSharedDataRepository>();
builder.Services.AddScoped<VendorTypeRepository>();
builder.Services.AddScoped<HeaderRepository>();
builder.Services.AddScoped<QuestionTypeRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
