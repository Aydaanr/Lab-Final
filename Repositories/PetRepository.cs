// ClothingItemRepository.cs
using System.Collections.Generic;
using System.Linq;

public class PetRepository
{
    private readonly AppDbContext _context;

    public PetRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Pet> GetAll()
    {
        return _context.Pets.ToList();
    }

    // ... add other methods for interacting with the database
}

