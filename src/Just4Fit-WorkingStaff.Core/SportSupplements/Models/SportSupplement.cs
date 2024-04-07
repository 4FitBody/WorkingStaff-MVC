namespace Just4Fit_WorkingStaff.Core.SportSupplements.Models;

public class SportSupplement
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public string? ManufactureCountry { get; set; }

    public double? Quantity { get; set; }

     public bool IsApproved {get; set;} 


}