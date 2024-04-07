namespace Just4Fit_WorkingStaff.Presentation.Controllers;

using Just4Fit_WorkingStaff.Core.Exercises.Models;
using Just4Fit_WorkingStaff.Infrastructure.Exercises.Commands;
using Just4Fit_WorkingStaff.Infrastructure.Exercises.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class ExerciseController : Controller
{
    private readonly ISender sender;

    public ExerciseController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet]
    [ActionName("Index")]
    public async Task<IActionResult> GetAll()
    {
        var getAllQuery = new GetAllQuery();

        var exercises = await this.sender.Send(getAllQuery);

        return base.View(model: exercises);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] Exercise exercise)
    {
        var createCommand = new CreateCommand(exercise);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int? id)
    {
        var createCommand = new DeleteCommand(id);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }

    [HttpPut]
    public async Task<IActionResult> Update(int? id, [FromBody] Exercise exercise)
    {
        var createCommand = new UpdateCommand(id, exercise);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }
}