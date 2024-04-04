namespace Just4Fit_WorkingStaff.Infrastructure.Exercises.Commands;

using Just4Fit_WorkingStaff.Core.Exercises.Models;
using MediatR;

public class UpdateCommand : IRequest
{
    public string? Id { get; set; }
    public Exercise? Exercise { get; set; }

    public UpdateCommand(string? id, Exercise? exercise)
    {
        this.Id = id;

        this.Exercise = exercise;
    }

    public UpdateCommand() {}
}