namespace Just4Fit_WorkingStaff.Infrastructure.Exercises.Commands;

using MediatR;

public class DeleteCommand : IRequest
{
    public string? Id { get; set; }

    public DeleteCommand(string? id) => this.Id = id;

    public DeleteCommand() {}
}