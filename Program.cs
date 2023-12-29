using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using LabFinal.Models;
using LabFinal; 

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add database context
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        }));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();



builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set the desired expiration time
    options.LoginPath = "/Identity/Login";
    options.LogoutPath = "/Identity/Logout";
    options.AccessDeniedPath = "/Identity/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddCookie(options =>
{
    options.Cookie.Name = "LabFinal";
    options.LoginPath = "/Identity/Login"; // Set the login Path
    options.AccessDeniedPath = "/Identity/AccessDenied"; // Set the access denied Path
});


builder.Services.AddAuthorization(options =>
{
    // Your authorization policies go here
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public static class DatabaseInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Pets.Any())
        {
            var pets = new List<Pet>
            {
                new Pet {
                        Town = "Ankara",
                        Type = "CAT",
                        AdoptionType = "ADOPTION",
                        Breed = "British",
                        Age = "Young",
                        Size = "Large",
                        Gender = "Male",
                        GoodWith = "Young",
                        CoatLength = "long",
                        Color = "Gray",
                        CareAndBehavior = "House-Trained",
                        Name = "Chiko",
                        Price = 0,
                        Description = "A playful and friendly cat named Chiko.",
                        ImageUrl = "/images/chiko.jpg"
                    },
                new Pet {
                        Town = "Ankara",
                        Type = "CAT",
                        AdoptionType = "ADOPTION",
                        Breed = "American Wirehair",
                        Age = "Young",
                        Size = "Small",
                        Gender = "Male",
                        GoodWith = "Young",
                        CoatLength = "Short",
                        Color = "White",
                        CareAndBehavior = "House-Trained",
                        Name = "Tommy",
                        Price = 0,
                        Description = "A playful and friendly cat named Tommy.",
                        ImageUrl = "/images/tommy.jpg"
                    },
                new Pet
                    {
                        Town = "Istanbul",
                        Type = "CAT",
                        AdoptionType = "ADOPTION",
                        Breed = "Siamese",
                        Age = "Young",
                        Size = "Medium",
                        Gender = "Male",
                        GoodWith = "Kids",
                        CoatLength = "Short",
                        Color = "Gray",
                        CareAndBehavior = "House-Trained",
                        Name = "Siamese",
                        Price = 0,
                        Description = "A playful and friendly cat named Siamese.",
                        ImageUrl = "/images/Siamese.jpg"
                    },
                    new Pet
                    {
                        Town = "Istanbul",
                        Type = "CAT",
                        AdoptionType = "ADOPTION",
                        Breed = "Persian",
                        Age = "Young",
                        Size = "Medium",
                        Gender = "Male",
                        GoodWith = "Kids",
                        CoatLength = "Medium",
                        Color = "White",
                        CareAndBehavior = "House-Trained",
                        Name = "Whiskers",
                        Price = 0,
                        Description = "Whiskers is a friendly Persian cat with a medium-length coat.",
                        ImageUrl = "/images/whiskers.jpg"
                    },
                    new Pet
                    {
                        Town = "Ankara",
                        Type = "CAT",
                        AdoptionType = "ADOPTION",
                        Breed = "Maine Coon",
                        Age = "Adult",
                        Size = "Large",
                        Gender = "Female",
                        GoodWith = "Other animals",
                        CoatLength = "Long",
                        Color = "Tabby",
                        CareAndBehavior = "Special-needs",
                        Name = "Mittens",
                        Price = 0,
                        Description = "Mittens is a sweet Maine Coon cat with a long coat.",
                        ImageUrl = "/images/mittens.jpg"
                    },
                    new Pet
                    {
                        Town = "Izmir",
                        Type = "CAT",
                        AdoptionType = "SELL",
                        Breed = "Bengal",
                        Age = "Baby",
                        Size = "Small",
                        Gender = "Male",
                        GoodWith = "Kids",
                        CoatLength = "Short",
                        Color = "Spotted",
                        CareAndBehavior = "Declawed",
                        Name = "Leo",
                        Price = 150.00m,
                        Description = "Leo is an adorable Bengal kitten with a short spotted coat.",
                        ImageUrl = "/images/leo.jpg"
                    },
                    new Pet
                    {
                        Town = "Bursa",
                        Type = "CAT",
                        AdoptionType = "SELL",
                        Breed = "Sphynx",
                        Age = "Senior",
                        Size = "Small",
                        Gender = "Female",
                        GoodWith = "Kids",
                        CoatLength = "Hairless",
                        Color = "Pink",
                        CareAndBehavior = "Special-needs",
                        Name = "Princess",
                        Price = 120.00m,
                        Description = "Princess is a regal Sphynx cat with a hairless coat.",
                        ImageUrl = "/images/princess.jpg"
                    },

                    new Pet
                        {
                            Type = "DOG",
                            Color = "Black",
                            Size = "Medium",
                            Breed = "Labrador Retriever",
                            Age = "Young",
                            Gender = "Female",
                            GoodWith = "Kids",
                            CoatLength = "Short",
                            Town = "Ankara",
                            AdoptionType = "ADOPTION",
                            CareAndBehavior = "House-Trained",
                            Name = "Oreo",
                            Price = 0,
                            Description = "Loving and energetic dog",
                            ImageUrl = "/images/oreo.jpg"
                        },
                        new Pet
                        {
                            Type = "DOG",
                            Color = "Brown",
                            Size = "Large",
                            Breed = "Golden Retriever",
                            Age = "Young",
                            Gender = "Male",
                            GoodWith = "Kids,Other animals",
                            CoatLength = "Long",
                            Town = "Izmir",
                            AdoptionType = "ADOPTION",
                            CareAndBehavior = "House-Trained",
                            Name = "Buddy",
                            Price = 0,
                            Description = "Friendly and affectionate",
                            ImageUrl = "/images/buddy.jpg"
                        },
                        new Pet
                        {
                            Type = "DOG",
                            Color = "White",
                            Size = "Small",
                            Breed = "Pomeranian",
                            Age = "Baby",
                            Gender = "Male",
                            GoodWith = "Kids",
                            CoatLength = "Fluffy",
                            Town = "Antalya",
                            AdoptionType = "SELL",
                            CareAndBehavior = "House-Trained",
                            Name = "Max",
                            Price = 150.00m,
                            Description = "Adorable and playful puppy",
                            ImageUrl = "/images/max.jpg"
                        },
                        new Pet
                        {
                            Type = "DOG",
                            Color = "Gray",
                            Size = "Medium",
                            Breed = "Siberian Husky",
                            Age = "Young",
                            Gender = "Female",
                            GoodWith = "Other animals",
                            CoatLength = "Medium",
                            Town = "Bursa",
                            AdoptionType = "SELL",
                            CareAndBehavior = "House-Trained",
                            Name = "Luna",
                            Price = 180.00m,
                            Description = "Beautiful and lively husky",
                            ImageUrl = "/images/luna.jpg"
                        },
                        new Pet
                        {
                            Type = "DOG",
                            Color = "Brown",
                            Size = "Large",
                            Breed = "Boxer",
                            Age = "Adult",
                            Gender = "Male",
                            GoodWith = "Kids",
                            CoatLength = "Short",
                            Town = "Adana",
                            AdoptionType = "ADOPTION",
                            CareAndBehavior = "House-Trained",
                            Name = "Rocky",
                            Price = 0,
                            Description = "Playful and protective",
                            ImageUrl = "/images/rocky.jpg"
                        },
                        new Pet
                        {
                            Type = "DOG",
                            Color = "Brown",
                            Size = "Medium",
                            Breed = "Cocker Spaniel",
                            Age = "Adult",
                            Gender = "Female",
                            GoodWith = "Kids,Other animals",
                            CoatLength = "Medium",
                            Town = "Gaziantep",
                            AdoptionType = "SELL",
                            CareAndBehavior = "House-Trained",
                            Name = "Coco",
                            Price = 130.00m,
                            Description = "Sweet and friendly spaniel",
                            ImageUrl = "/images/coco.jpg"
                        },
                        new Pet
                        {
                            Type = "DOG",
                            Color = "Black",
                            Size = "Small",
                            Breed = "Poodle",
                            Age = "Senior",
                            Gender = "Male",
                            GoodWith = "Other animals",
                            CoatLength = "Curly",
                            Town = "Trabzon",
                            AdoptionType = "ADOPTION",
                            CareAndBehavior = "House-Trained",
                            Name = "Charlie",
                            Price = 0,
                            Description = "Gentle and calm senior dog",
                            ImageUrl = "/images/charlie.jpg"
                        },
                        new Pet
                        {
                            Type = "DOG",
                            Color = "White",
                            Size = "Medium",
                            Breed = "Maltese",
                            Age = "Young",
                            Gender = "Female",
                            GoodWith = "Kids,Other animals",
                            CoatLength = "Long",
                            Town = "Samsun",
                            AdoptionType = "SELL",
                            CareAndBehavior = "House-Trained",
                            Name = "Daisy",
                            Price = 160.00m,
                            Description = "Adorable and playful maltese",
                            ImageUrl = "/images/daisy.jpg"
                        },
                        new Pet
                        {
                            Type = "DOG",
                            Color = "Black",
                            Size = "Large",
                            Breed = "German Shepherd",
                            Age = "Adult",
                            Gender = "Male",
                            GoodWith = "Kids,Other animals",
                            CoatLength = "Short",
                            Town = "Istanbul",
                            AdoptionType = "ADOPTION",
                            CareAndBehavior = "House-Trained",
                            Name = "Maximus",
                            Price = 0,
                            Description = "Intelligent and loyal shepherd",
                            ImageUrl = "/images/maximus.jpg"
                        },
                        new Pet
                        {
                            Type = "DOG",
                            Color = "Golden",
                            Size = "Medium",
                            Breed = "Golden Retriever",
                            Age = "Young",
                            Gender = "Female",
                            GoodWith = "Kids,Other animals",
                            CoatLength = "Long",
                            Town = "Izmir",
                            AdoptionType = "ADOPTION",
                            CareAndBehavior = "House-Trained",
                            Name = "Lucy",
                            Price = 0,
                            Description = "Friendly and affectionate retriever",
                            ImageUrl = "/images/lucy.jpg"
                        },
                        new Pet
                        {
                            Type = "DOG",
                            Color = "Brown",
                            Size = "Medium",
                            Breed = "Dachshund",
                            Age = "Adult",
                            Gender = "Male",
                            GoodWith = "Kids",
                            CoatLength = "Short",
                            Town = "Antalya",
                            AdoptionType = "SELL",
                            CareAndBehavior = "House-Trained",
                            Name = "Rusty",
                            Price = 120.00m,
                            Description = "Playful and energetic dachshund",
                            ImageUrl = "/images/rusty.jpg"
                        }, 


                        new Pet
                            {
                                Type = "BIRD",
                                Color = "Green",
                                Size = "Small",
                                Breed = "Budgerigar",
                                Age = "Young",
                                Gender = "Male",
                                GoodWith = "Other animals",
                                CoatLength = "Feathers",
                                Town = "Istanbul",
                                AdoptionType = "ADOPTION",
                                CareAndBehavior = "Sings melodious tunes",
                                Name = "Kiwi",
                                Price = 0,
                                Description = "Friendly and chirpy budgie",
                                ImageUrl = "/images/kiwi.jpg"
                            },
                            new Pet
                            {
                                Type = "BIRD",
                                Color = "Blue",
                                Size = "Small",
                                Breed = "Lovebird",
                                Age = "Adult",
                                Gender = "Female",
                                GoodWith = "Kids",
                                CoatLength = "Feathers",
                                Town = "Ankara",
                                AdoptionType = "SELL",
                                CareAndBehavior = "Loves to mimic sounds",
                                Name = "Sky",
                                Price = 40.00m,
                                Description = "Beautiful and talkative lovebird",
                                ImageUrl = "/images/sky.jpg"
                            },                                           

                            new Pet
                                {
                                    Type = "HORSE",
                                    Color = "Brown",
                                    Size = "Large",
                                    Breed = "Thoroughbred",
                                    Age = "Adult",
                                    Gender = "Male",
                                    GoodWith = "Kids",
                                    CoatLength = "Short",
                                    Town = "Izmir",
                                    AdoptionType = "ADOPTION",
                                    CareAndBehavior = "Loves to run",
                                    Name = "Thunder",
                                    Price = 0,
                                    Description = "Energetic and majestic thoroughbred",
                                    ImageUrl = "/images/thunder.jpg"
                                },                            

            };           
            context.Pets.AddRange(pets);
            context.SaveChanges();
        }

    }
}
