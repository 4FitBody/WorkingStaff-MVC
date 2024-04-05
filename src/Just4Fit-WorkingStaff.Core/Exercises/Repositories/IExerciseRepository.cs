namespace Just4Fit_WorkingStaff.Core.Exercises.Repositories;

using Just4Fit_WorkingStaff.Core.Exercises.Models;

public interface IExerciseRepository
{
    Task<IEnumerable<Exercise>?> GetAllAsync();
    Task CreateAsync(Exercise exercise);
    Task DeleteAsync(string id);
    Task UpdateAsync(string id, Exercise exercise);
}