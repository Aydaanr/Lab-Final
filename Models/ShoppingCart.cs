using LabFinal.Models;

public class ShoppingCart
{
    public int Id { get; set; }

    public string UserId { get; set; } // Add this property for user ID
    public virtual ApplicationUser User { get; set; }
    public int PetId { get; set; }
    public Pet Pet { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice => Pet?.Price * Quantity ?? 0;
}
