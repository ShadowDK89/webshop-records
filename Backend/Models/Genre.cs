using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Genre
{
    public Genre()
    {
        this.Products = new HashSet<Product>();
    }
    public int Id { get; set; }
    public string GenreName { get; set; }
    [JsonIgnore]
    public ICollection<Product> Products { get; set; }

    [JsonIgnore]
    public List<ProductGenre> ProductGenres { get; set; }
}