using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ProductFormat
{
    public int ProductId { get; set; }
    public int FormatId { get; set; }
    public Format Format { get; set; }

    public Product Product { get; set; }
}