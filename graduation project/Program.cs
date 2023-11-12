using graduationProject.DAL;
using GraduationProject.BL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServiceStack;

var builder = WebApplication.CreateBuilder(args);
const string AllowAllPolicy = "AllowAllPolicy";

// Add services to the container.
#region Default

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region DataBase
string? connectionString = builder.Configuration.GetConnectionString("Hospital");
builder.Services.AddDbContext<HospitalContext>(options =>
    options.UseSqlServer(connectionString));
#endregion
#region Asp Identity 
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<HospitalContext>();
#endregion


#region Repos

builder.Services.AddScoped<IPatientRepo, PatientRepo>();

builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
builder.Services.AddScoped<IVisitReviewAndRateRepo, VisitReviewAndRateRepo>();
builder.Services.AddScoped<IWeekScheduleRepo, WeekScheduleRepo>();
builder.Services.AddScoped<IPatientVisitRepo, PatientVisitRepo>();

#endregion

#region Unit of work

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion
#region Managers
builder.Services.AddScoped<IPatientManager, PatientManager>();

builder.Services.AddScoped<IDoctorManager, DoctorManager>();

#endregion
#region cors
builder.Services.AddCors(Options =>
{
    Options.AddPolicy(AllowAllPolicy, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(AllowAllPolicy);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
