namespace Just4Fit_WorkingStaff.Core.Exercises.Models;

public class Exercise
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? BodyPart { get; set; }

    public string? Equipment { get; set; }

    public string? GifUrl { get; set; }

    public string? Target { get; set; }

    public string?[]? SecondaryMuscles { get; set; }

    public string?[]? Instructions { get; set; }
}