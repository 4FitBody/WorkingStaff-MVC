using System.Reflection;
using Just4Fit_WorkingStaff.Core.Exercises.Repositories;
using Just4Fit_WorkingStaff.Core.Food.Repositories;
using Just4Fit_WorkingStaff.Infrastructure.Data;
using Just4Fit_WorkingStaff.Infrastructure.Exercises.Repositories;
using Just4Fit_WorkingStaff.Infrastructure.Food.Repositories;
using Just4Fit_WorkingStaff.Core.News.Models;
using Just4Fit_WorkingStaff.Core.News.Repositories;
using Just4Fit_WorkingStaff.Infrastructure.News.Repositories;
using Just4Fit_WorkingStaff.Core.SportSupplements.Models.Repositories;
using Just4Fit_WorkingStaff.Infrastructure.SportSupplements.Repositories;
using Just4Fit_WorkingStaff.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var infrastructureAssembly = typeof(JustForFitWorkingStaffDbContext).Assembly;

builder.Services.AddMediatR(configurations =>
{
    configurations.RegisterServicesFromAssembly(infrastructureAssembly);
});

builder.Services.AddScoped<IExerciseRepository, ExerciseSqlRepository>();

builder.Services.AddScoped<IFoodRepository, FoodSqlRepository>();

builder.Services.AddScoped<INewsRepository, NewsSqlRepository>();

builder.Services.AddScoped<ISportSupplementRepository, SportSupplementRepository>();

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("JustForFitDb");

builder.Services.AddDbContext<JustForFitWorkingStaffDbContext>(dbContextOptionsBuilder =>
{
    dbContextOptionsBuilder.UseNpgsql(connectionString, o =>
    {
        o.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    });
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
})
    .AddEntityFrameworkStores<JustForFitWorkingStaffDbContext>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();