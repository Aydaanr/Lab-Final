public class UserSelectionViewModel
{
    public List<Pet> Pets { get; set; }
    public string TownFilter { get; set; }
    public string NameFilter { get; set; }
    public string TypeFilter { get; set; }
    public string SizeFilter { get; set; }
    public string ColorFilter { get; set; }
    public string GenderFilter { get; set; }
    public string BreedFilter { get; set; }
    public string AgeFilter { get; set; }
    public string AdoptionTypeFilter { get; set; }
    public string GoodWithFilter { get; set; }
    public string CoatLengthFilter { get; set; }
    public string CareBehaviorFilter { get; set; }

    // Properties for dropdowns (e.g., colors, genders)
    public List<string> Types { get; set; }
    public List<string> Colors { get; set; }
    public List<string> Genders { get; set; }
    public List<string> Sizes { get; set; }
    public List<string> Breeds { get; set; }
    public List<string> Ages { get; set; }
    public List<string> AdoptionTypes { get; set; }
    public List<string> GoodWithOptions { get; set; }
    public List<string> CoatLengths { get; set; }
    public List<string> CareBehaviors { get; set; }

    public UserSelectionViewModel()
    {
        // Initialize lists as needed
        Pets = new List<Pet>();
        Types = new List<string> { "CATS", "DOGS", "BIRDS", "HORSES", "RABBITS", "REPTILES", "OTHERS" };
        Colors = new List<string> { "Blue", "Red", "Green", "Yellow", "Orange", "Pink", "Purple", "Brown", "Black", "White", "Gray", "Tan", "Cream", "Gold", "Silver", "Lavender", "Indigo", "Teal", "Maroon", "Cyan" };
        Genders = new List<string> { "Male", "Female" };
        Sizes = new List<string> { "Small", "Medium", "Large", "Extra Large" };
        Ages = new List<string> { "Baby", "Young", "Adult", "Senior" };
        AdoptionTypes = new List<string> { "Adoption", "Sell" };
        GoodWithOptions = new List<string> { "Kids", "Other animals" };
        CoatLengths = new List<string> { "Hairless", "Short", "Medium", "Long" };
        CareBehaviors = new List<string> { "House-Trained", "Declawed", "Special-needs" };
    }
}
