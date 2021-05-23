using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ProductGenre
{
    public int ProductId { get; set; }
    public int GenreId { get; set; }

    public Product Product { get; set; }
    public Genre Genre { get; set; }

}