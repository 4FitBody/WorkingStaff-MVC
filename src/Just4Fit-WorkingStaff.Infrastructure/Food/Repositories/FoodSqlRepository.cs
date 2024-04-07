namespace Just4Fit_WorkingStaff.Infrastructure.Food.Repositories;

using Just4Fit_WorkingStaff.Core.Food.Repositories;
using Just4Fit_WorkingStaff.Core.Food.Models;
using Just4Fit_WorkingStaff.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class FoodSqlRepository : IFoodRepository
{
    private readonly JustForFitWorkingStaffDbContext dbContext;

    public FoodSqlRepository(JustForFitWorkingStaffDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(Food food)
    {
        await this.dbContext.Food.AddAsync(food);

        await this.dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var foodToDelete = await this.dbContext.Food.FirstOrDefaultAsync(food => food.Id == id);

        this.dbContext.Remove<Food>(foodToDelete!);

        await this.dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Food>?> GetAllAsync()
    {
        var food = this.dbContext.Food.AsEnumerable();

        return food;
    }

    public async Task UpdateAsync(int id, Food food)
    {
        var oldFood = await this.dbContext.Food.FirstOrDefaultAsync(f => f.Id == id);

#pragma warning disable CS8602
        oldFood.Name = food.Name;
#pragma warning restore CS8602
        oldFood.Diet = food.Diet;
        oldFood.Description = food.Description;
        oldFood.ImageUrl = food.ImageUrl;
        oldFood.VideoUrl = food.VideoUrl;
        oldFood.IsApproved = food.IsApproved;

        await this.dbContext.SaveChangesAsync();
    }
}
