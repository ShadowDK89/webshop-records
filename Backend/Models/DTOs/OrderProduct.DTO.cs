
public class OrderProductDTO
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public int Price { get; set; }
    public int ColorId { get; set; }
    public int FormatId { get; set; }
    public Product Product { get; set; }
    public Color Color { get; set; }
    public Format Format { get; set; }

}