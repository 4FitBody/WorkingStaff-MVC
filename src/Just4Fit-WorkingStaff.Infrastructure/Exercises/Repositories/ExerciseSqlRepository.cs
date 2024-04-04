namespace Just4Fit_WorkingStaff.Infrastructure.Exercises.Repositories;

using Just4Fit_WorkingStaff.Core.Exercises.Models;
using Just4Fit_WorkingStaff.Core.Exercises.Repositories;
using Just4Fit_WorkingStaff.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ExerciseSqlRepository : IExerciseRepository
{
    private readonly JustForFitWorkingStaffDbContext dbContext;

    public ExerciseSqlRepository(JustForFitWorkingStaffDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Exercise>?> GetAllAsync()
    {
        var exercises = this.dbContext.Exercises.AsEnumerable();

        return exercises;
    }

    public async Task CreateAsync(Exercise exercise)
    {
        await this.dbContext.Exercises.AddAsync(exercise);
    }

    public async Task DeleteAsync(string id)
    {
        var exerciseToDelete = await this.dbContext.Exercises.FirstOrDefaultAsync(exercise => exercise.Id == id);
    
        this.dbContext.Remove<Exercise>(exerciseToDelete!);
    }

    public async Task UpdateAsync(string id, Exercise exercise)
    {
        var oldExercise = await this.dbContext.Exercises.FirstOrDefaultAsync(e => e.Id == id);
    
        oldExercise.Name = exercise.Name;
        oldExercise.Equipment = exercise.Equipment;
        oldExercise.Instructions = exercise.Instructions;
        oldExercise.Target = exercise.Target;
        oldExercise.GifUrl = exercise.GifUrl;
        oldExercise.BodyPart = exercise.BodyPart;
        oldExercise.SecondaryMuscles = exercise.SecondaryMuscles;

        await this.dbContext.SaveChangesAsync();
    }
}