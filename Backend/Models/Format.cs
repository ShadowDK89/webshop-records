using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Format
{
    public Format()
    {
        this.Products = new HashSet<Product>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public ICollection<Product> Products { get; set; }

    [JsonIgnore]
    public List<ProductFormat> ProductFormats { get; set; }
}