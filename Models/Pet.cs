using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pet
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Town is required.")]
    public string Town { get; set; }

    [Required(ErrorMessage = "Type is required.")]
    public string Type { get; set; }

    [Required(ErrorMessage = "Adoption type is required.")]
    public string AdoptionType { get; set; }

    [Required(ErrorMessage = "Breed is required.")]
    public string Breed { get; set; }

    [Required(ErrorMessage = "Age is required.")]
    public string Age { get; set; }

    [Required(ErrorMessage = "Size is required.")]
    public string Size { get; set; }

    [Required(ErrorMessage = "Gender is required.")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "Good with is required.")]
    public string GoodWith { get; set; }

    [Required(ErrorMessage = "Coat length is required.")]
    public string CoatLength { get; set; }

    [Required(ErrorMessage = "Color is required.")]
    public string Color { get; set; }

    [Required(ErrorMessage = "Care and behavior is required.")]
    public string CareAndBehavior { get; set; }

    [Required(ErrorMessage = "Pet name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "ImageUrl is required.")]
    public string ImageUrl { get; set; }
}
