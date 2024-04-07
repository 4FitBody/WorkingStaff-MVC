namespace Just4Fit_WorkingStaff.Infrastructure.SportSupplements.Repositories;

using Just4Fit_WorkingStaff.Core.SportSupplements.Models;
using Just4Fit_WorkingStaff.Core.SportSupplements.Models.Repositories;
using Just4Fit_WorkingStaff.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


public class SportSupplementRepository : ISportSupplementRepository
{
    private readonly JustForFitWorkingStaffDbContext dbContext;

    public SportSupplementRepository(JustForFitWorkingStaffDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<SportSupplement>?> GetAllAsync()
    {
        var supplements = await this.dbContext.SportSupplements.ToListAsync();

        return supplements;
    }

    public async Task CreateAsync(SportSupplement supplement)
    {
        await dbContext.SportSupplements.AddAsync(supplement);

        await dbContext.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var supplementforDelete = await dbContext.SportSupplements.FindAsync(id);

        if (supplementforDelete != null)
        {
            dbContext.SportSupplements.Remove(supplementforDelete);

            await dbContext.SaveChangesAsync();
        }
    }


    public async Task UpdateAsync(int id, SportSupplement updatedSupplement)
    {
        if (updatedSupplement == null)
        {
            throw new ArgumentNullException(nameof(updatedSupplement));
        }

        if (id != updatedSupplement.Id)
        {
            throw new ArgumentException("There is no Such Sport Supplement", nameof(id));
        }

        var existingSupplement = await dbContext.SportSupplements.FindAsync(id);

        if (existingSupplement == null)
        {
            throw new ArgumentException("Supplement not found", nameof(id));
        }

        existingSupplement.Name = updatedSupplement.Name;
        existingSupplement.ImageUrl = updatedSupplement.ImageUrl;
        existingSupplement.Description = updatedSupplement.Description;
        existingSupplement.ManufactureCountry = updatedSupplement.ManufactureCountry;
        existingSupplement.Quantity = updatedSupplement.Quantity;

        await dbContext.SaveChangesAsync();
    }

}
