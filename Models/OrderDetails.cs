using ProjektZespolowy.Models;
using System.ComponentModel.DataAnnotations;

public class OrderDetails
{

    [Key]
    public int OrderId { get; set; } 
    public int ProductId { get; set; } 

    public Order? Order { get; set; }
    public Product? Product { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być większa od 0.")]
    public int Quantity { get; set; }
}
