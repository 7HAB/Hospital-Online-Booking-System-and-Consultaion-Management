using graduationProject.DAL;
using GraduationProject.BL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddIdentity<Patient, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<HospitalContext>();
#endregion


#region Repos

builder.Services.AddScoped<IPatientRepo, PatientRepo>();

#endregion
#region Managers
builder.Services.AddScoped<IPatientManager, PatientManager>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
