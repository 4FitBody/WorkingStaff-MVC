using Just4Fit_WorkingStaff.Core.Exercises.Models;

namespace Just4Fit_WorkingStaff.Core.Exercises.Repositories;

public interface IExerciseRepository
{
    Task<IEnumerable<Exercise>?> GetAllAsync();
    Task CreateAsync(Exercise exercise);
    Task DeleteAsync(string id);
    Task UpdateAsync(string id, Exercise exercise);
}