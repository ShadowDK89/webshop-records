using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class OrderProduct
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public int Price { get; set; }
    [JsonIgnore]
    public Order Order { get; set; }
    [JsonIgnore]
    public Product Product { get; set; }
    public int ColorId { get; set; }
    public Color Color { get; set; }
    public int FormatId { get; set; }
    public Format Format { get; set; }
}