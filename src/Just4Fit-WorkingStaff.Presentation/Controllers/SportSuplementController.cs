namespace Just4Fit_WorkingStaff.Presentation.Controllers;

using Just4Fit_WorkingStaff.Core.SportSupplements.Models;
using Just4Fit_WorkingStaff.Infrastructure.SportSupplements.Commands;
using Just4Fit_WorkingStaff.Infrastructure.SportSupplements.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


public class SportSuplementController : Controller
{
    private readonly ISender sender;

    public SportSuplementController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet]
    [ActionName("Index")]
    public async Task<IActionResult> GetAll()
    {
        var getAllQuery = new GetAllQuery();

        var suplements = await this.sender.Send(getAllQuery);

        return base.View(model: suplements);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] SportSupplement sportSupplement)
    {
        var createCommand = new CreateCommand(sportSupplement);

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
    public async Task<IActionResult> Update(int? id, [FromBody] SportSupplement sportSupplement)
    {
        var createCommand = new UpdateCommand(id, sportSupplement);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }
}
