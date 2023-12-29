using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LabFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace LabFinal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager; 
        
    public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            DatabaseInitializer.Initialize(_context);
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Detail(int id)
    {
        var pet = _context.Pets.Find(id);
        return View(pet);
    }

    [HttpPost]
[HttpPost]
public IActionResult AddToCart(int id, int quantity)
{
    var pet = _context.Pets.Find(id);

    if (pet == null)
    {
        // Handle the case where the item with the given id is not found
        return RedirectToAction("Index");
    }

    // Get the current user's ID
    var userId = _userManager.GetUserId(User);

    // Create a new shopping cart item
    var shoppingCartItem = new ShoppingCart
    {
        Pet = pet,
        Quantity = 1,
        UserId = userId
    };

    // Add the item to the shopping cart
    _context.ShoppingCarts.Add(shoppingCartItem);
    _context.SaveChanges();

    // Redirect to the shopping cart or another page
    return RedirectToAction("Index");
}

    public IActionResult Payment()
    {
        // Logic for displaying payment page
        var shoppingCart = _context.ShoppingCarts.Include(sc => sc.Pet).ToList();
        return View(shoppingCart);
    }

    public IActionResult ConfirmPayment()
    {
        // Logic for confirming payment
        return RedirectToAction("Index");
    }    


    // [Authorize]
    // public IActionResult Index(UserSelectionViewModel viewModel)
    // {
    //     var query = _context.Pets.AsQueryable();

    //     // Apply filters
    //     if (!string.IsNullOrEmpty(viewModel.TownFilter))
    //     {
    //         query = query.Where(c => c.Town.Contains(viewModel.TownFilter));
    //     }

    //     if (!string.IsNullOrEmpty(viewModel.NameFilter))
    //     {
    //         query = query.Where(c => c.Name.Contains(viewModel.NameFilter));
    //     }

    //     if (!string.IsNullOrEmpty(viewModel.TypeFilter))
    //     {
    //         query = query.Where(c => c.Type.Contains(viewModel.TypeFilter));
    //     }

    //     if (!string.IsNullOrEmpty(viewModel.SizeFilter))
    //     {
    //         query = query.Where(c => c.Size.Contains(viewModel.SizeFilter));
    //     }

    //     if (!string.IsNullOrEmpty(viewModel.ColorFilter))
    //     {
    //         query = query.Where(c => c.Color.Contains(viewModel.ColorFilter));
    //     }

    //     if (!string.IsNullOrEmpty(viewModel.GenderFilter))
    //     {
    //         query = query.Where(c => c.Gender.Contains(viewModel.GenderFilter));
    //     }

    //     if (!string.IsNullOrEmpty(viewModel.BreedFilter))
    //     {
    //         query = query.Where(c => c.Breed.Contains(viewModel.BreedFilter));
    //     }

    //     if (!string.IsNullOrEmpty(viewModel.AgeFilter))
    //     {
    //         query = query.Where(c => c.Age.Contains(viewModel.AgeFilter));
    //     }

    //     if (!string.IsNullOrEmpty(viewModel.AdoptionTypeFilter))
    //     {
    //         query = query.Where(c => c.AdoptionType.Contains(viewModel.AdoptionTypeFilter));
    //     }

    //     if (!string.IsNullOrEmpty(viewModel.GoodWithFilter))
    //     {
    //         query = query.Where(c => c.GoodWith.Contains(viewModel.GoodWithFilter));
    //     }

    //     if (!string.IsNullOrEmpty(viewModel.CoatLengthFilter))
    //     {
    //         query = query.Where(c => c.CoatLength.Contains(viewModel.CoatLengthFilter));
    //     }

    //     if (!string.IsNullOrEmpty(viewModel.CareBehaviorFilter))
    //     {
    //         query = query.Where(c => c.CareAndBehavior.Contains(viewModel.CareBehaviorFilter));
    //     }

    //     // Get the filtered data
    //     viewModel.Pets = query.ToList();

    //     // Populate dropdowns for the view
    //     viewModel.Types = _context.Pets.Select(c => c.Type).Distinct().ToList();
    //     viewModel.Sizes = _context.Pets.Select(c => c.Size).Distinct().ToList();
    //     viewModel.Colors = _context.Pets.Select(c => c.Color).Distinct().ToList();
    //     viewModel.Genders = _context.Pets.Select(c => c.Gender).Distinct().ToList();
    //     viewModel.Breeds = _context.Pets.Select(c => c.Breed).Distinct().ToList();
    //     viewModel.Ages = _context.Pets.Select(c => c.Age).Distinct().ToList();
    //     viewModel.AdoptionTypes = _context.Pets.Select(c => c.AdoptionType).Distinct().ToList();
    //     viewModel.GoodWithOptions = _context.Pets.Select(c => c.GoodWith).Distinct().ToList();
    //     viewModel.CoatLengths = _context.Pets.Select(c => c.CoatLength).Distinct().ToList();
    //     viewModel.CareBehaviors = _context.Pets.Select(c => c.CareAndBehavior).Distinct().ToList();

    //     return View(viewModel);
    // }

[Authorize]
public IActionResult Index(UserSelectionViewModel viewModel)
{
var query = _context.Pets.AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(viewModel.TownFilter))
        {
            query = query.Where(c => c.Town.Contains(viewModel.TownFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.NameFilter))
        {
            query = query.Where(c => c.Name.Contains(viewModel.NameFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.TypeFilter))
        {
            query = query.Where(c => c.Type.Contains(viewModel.TypeFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.SizeFilter))
        {
            query = query.Where(c => c.Size.Contains(viewModel.SizeFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.ColorFilter))
        {
            query = query.Where(c => c.Color.Contains(viewModel.ColorFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.GenderFilter))
        {
            query = query.Where(c => c.Gender.Contains(viewModel.GenderFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.BreedFilter))
        {
            query = query.Where(c => c.Breed.Contains(viewModel.BreedFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.AgeFilter))
        {
            query = query.Where(c => c.Age.Contains(viewModel.AgeFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.AdoptionTypeFilter))
        {
            query = query.Where(c => c.AdoptionType.Contains(viewModel.AdoptionTypeFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.GoodWithFilter))
        {
            query = query.Where(c => c.GoodWith.Contains(viewModel.GoodWithFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.CoatLengthFilter))
        {
            query = query.Where(c => c.CoatLength.Contains(viewModel.CoatLengthFilter));
        }

        if (!string.IsNullOrEmpty(viewModel.CareBehaviorFilter))
        {
            query = query.Where(c => c.CareAndBehavior.Contains(viewModel.CareBehaviorFilter));
        }

        // Get the filtered data
        viewModel.Pets = query.ToList();


        viewModel.Types = _context.Pets.Select(c => c.Type).Distinct().ToList();
        viewModel.Sizes = _context.Pets.Select(c => c.Size).Distinct().ToList();
        viewModel.Colors = _context.Pets.Select(c => c.Color).Distinct().ToList();
        viewModel.Genders = _context.Pets.Select(c => c.Gender).Distinct().ToList();
        viewModel.Breeds = _context.Pets.Select(c => c.Breed).Distinct().ToList();
        viewModel.Ages = _context.Pets.Select(c => c.Age).Distinct().ToList();
        viewModel.AdoptionTypes = _context.Pets.Select(c => c.AdoptionType).Distinct().ToList();
        viewModel.GoodWithOptions = _context.Pets.Select(c => c.GoodWith).Distinct().ToList();
        viewModel.CoatLengths = _context.Pets.Select(c => c.CoatLength).Distinct().ToList();
        viewModel.CareBehaviors = _context.Pets.Select(c => c.CareAndBehavior).Distinct().ToList();


    // Set filter values in ViewData
    ViewData["NameFilter"] = viewModel.NameFilter;
    ViewData["TownFilter"] = viewModel.TownFilter;
    ViewData["BreedFilter"] = viewModel.BreedFilter;
    ViewData["TypeFilter"] = viewModel.TypeFilter;
    ViewData["SizeFilter"] = viewModel.SizeFilter;
    ViewData["ColorFilter"] = viewModel.ColorFilter;
    ViewData["GenderFilter"] = viewModel.GenderFilter;
    ViewData["AgeFilter"] = viewModel.AgeFilter;
    ViewData["AdoptionTypeFilter"] = viewModel.AdoptionTypeFilter;
    ViewData["GoodWithFilter"] = viewModel.GoodWithFilter;
    ViewData["CoatLengthFilter"] = viewModel.CoatLengthFilter;
    ViewData["CareBehaviorFilter"] = viewModel.CareBehaviorFilter;

    return View(viewModel);
}

    [HttpPost]
    [HttpGet]
    public IActionResult ShoppingCarts()
    {
        var shoppingCarts = _context.ShoppingCarts.Include(sc => sc.Pet).ToList();
        return View(shoppingCarts);
    }

   public IActionResult Checkout()
    {
        // Get the shopping cart items
        var shoppingCarts = _context.ShoppingCarts.Include(sc => sc.Pet).ToList();

        // Calculate total price
        decimal total = shoppingCarts.Sum(sc => sc.Pet.Price * sc.Quantity);

        // Pass the shopping carts and total price to the view
        ViewBag.ShoppingCarts = shoppingCarts;
        ViewBag.TotalPrice = total;

        return View();
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int id)
    {
        var shoppingCart = _context.ShoppingCarts.Find(id);

        if (shoppingCart != null)
        {
            _context.ShoppingCarts.Remove(shoppingCart);
            _context.SaveChanges();
        }

        return RedirectToAction("ShoppingCarts");
    }
}
