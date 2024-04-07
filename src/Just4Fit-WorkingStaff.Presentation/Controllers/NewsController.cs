namespace Just4Fit_WorkingStaff.Presentation.Controllers;

using Just4Fit_WorkingStaff.Core.News.Models;
using Just4Fit_WorkingStaff.Infrastructure.News.Commands;
using Just4Fit_WorkingStaff.Infrastructure.News.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class NewsController : Controller
{
    private readonly ISender sender;

    public NewsController(ISender sender)
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
    public async Task<IActionResult> Create([FromForm] News news)
    {
        var createCommand = new CreateCommand(news);

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
    public async Task<IActionResult> Update(int? id, [FromBody] News news)
    {
        var createCommand = new UpdateCommand(id, news);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }
}