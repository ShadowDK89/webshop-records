using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class ProductColor
{
    public int ProductId { get; set; }
    public int ColorId { get; set; }

    public Color Color { get; set; }
    public Product Product { get; set; }

}