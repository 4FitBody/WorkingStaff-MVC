namespace Just4Fit_WorkingStaff.Presentation.Controllers;

using Just4Fit_WorkingStaff.Core.Exercises.Models;
using Just4Fit_WorkingStaff.Infrastructure.Exercises.Commands;
using Just4Fit_WorkingStaff.Infrastructure.Exercises.Queries;
using Just4Fit_WorkingStaff.Infrastructure.Services;
using Just4Fit_WorkingStaff.Presentation.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

public class ExerciseController : Controller
{
    private readonly ISender sender;
    private readonly BlobContainerService blobContainerService;

    public ExerciseController(ISender sender)
    {
        this.sender = sender;

        this.blobContainerService = new BlobContainerService();
    }

    [HttpGet]
    [ActionName("Index")]
    public async Task<IActionResult> GetAll()
    {
        var getAllQuery = new GetAllQuery();

        var exercises = await this.sender.Send(getAllQuery);

        return base.View(model: exercises);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        var getByIdQuery = new GetByIdQuery(id);

        var exercise = await this.sender.Send(getByIdQuery);

        return base.View(model: exercise);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return base.View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ExerciseDto exerciseDto, IFormFile file)
    {
        var rawPath = Guid.NewGuid().ToString() + file.FileName;

        var path = rawPath.Replace(" ", "%20");

        var exercise = new Exercise
        {
            Name = exerciseDto.Name,
            BodyPart = exerciseDto.BodyPart,
            Equipment = exerciseDto.Equipment,
            Target = exerciseDto.Target,
            IsApproved = false,
            GifUrl = "https://4fitbodystorage.blob.core.windows.net/images/" + path,
            SecondaryMuscles = exerciseDto.SecondaryMuscles!.Split("; "),
            Instructions = exerciseDto.Instructions!.Split("; "),
        };

        await this.blobContainerService.UploadAsync(file.OpenReadStream(), rawPath);

        var createCommand = new CreateCommand(exercise);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }

    [Route("[controller]/[action]/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        var createCommand = new DeleteCommand(id);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }

    [HttpGet]
    [Route("[controller]/[action]/{id}")]
    public async Task<IActionResult> Update(int? id)
    {
        var getByIdQuery = new GetByIdQuery(id);

        var exercise = await this.sender.Send(getByIdQuery);

        return base.View(model: exercise);
    }

    [HttpPut]
    [Route("[controller]/[action]/{id}")]
    public async Task<IActionResult> Update(int? id, [FromBody] ExerciseDto exerciseDto)
    {
        var exercise = new Exercise
        {
            Name = exerciseDto.Name,
            BodyPart = exerciseDto.BodyPart,
            Equipment = exerciseDto.Equipment,
            Target = exerciseDto.Target,
            IsApproved = false,
            SecondaryMuscles = exerciseDto.SecondaryMuscles!.Split("; "),
            Instructions = exerciseDto.Instructions!.Split("; "),
        };

        var createCommand = new UpdateCommand(id, exercise);

        await this.sender.Send(createCommand);

        return base.RedirectToAction(actionName: "Index");
    }
}