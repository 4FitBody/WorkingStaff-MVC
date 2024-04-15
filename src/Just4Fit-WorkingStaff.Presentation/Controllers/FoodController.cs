namespace Just4Fit_WorkingStaff.Presentation.Controllers;

using System.Threading.Tasks;
using Just4Fit_WorkingStaff.Core.Food.Models;
using Just4Fit_WorkingStaff.Infrastructure.Food.Commands;
using Just4Fit_WorkingStaff.Infrastructure.Food.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class FoodController : Controller
{
    private readonly ISender sender;
    private readonly BlobContainerService blobContainerService;

    public FoodController(ISender sender)
    {
        this.sender = sender;

        this.blobContainerService = new BlobContainerService();
    }


    [HttpGet]
    [ActionName("Index")]
    public async Task<IActionResult> GetAll()
    {
        var getAllQuery = new GetAllQuery();

        var food = await this.sender.Send(getAllQuery);

        return base.View(model: food);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] Food food, IFormFile imageFile, IFormFile contentFile)
    {
        var rawPath = Guid.NewGuid().ToString() + imageFile.FileName;

        var path = rawPath.Replace(" ", "%20");

        food.ImageUrl = "https://4fitbodystorage.blob.core.windows.net/images/" + path;

        await this.blobContainerService.UploadAsync(imageFile.OpenReadStream(), rawPath);

        var VideoRawPath = Guid.NewGuid().ToString() + contentFile.FileName;

        var videoPath = rawPath.Replace(" ", "%20");

        food.VideoUrl = "https://4fitbodystorage.blob.core.windows.net/videos/" + videoPath;

        await this.blobContainerService.UploadAsync(contentFile.OpenReadStream(), VideoRawPath);

        var createCommand = new CreateCommand(food);

        await this.sender.Send(createCommand);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return base.View();
    }

    public async Task<IActionResult> Delete(int? id)
    {
        var createCommand = new DeleteCommand(id);

        await this.sender.Send(createCommand);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(int? id, [FromBody] Food food)
    {
        var createCommand = new UpdateCommand(id, food);

        await this.sender.Send(createCommand);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var getByIdQuery = new GetByIdQuery(id);

        var food = await this.sender.Send(getByIdQuery);

        return View(food);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var getByIdQuery = new GetByIdQuery(id);

        var food = await this.sender.Send(getByIdQuery);

        return View(food);
    }
}
