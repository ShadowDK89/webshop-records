using System;
using System.Collections.Generic;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string PaymentMethod { get; set; }
    public int TotalPrice { get; set; }
    public int CustomerId { get; set; }
    public List<OrderProductDTO> OrderProducts { get; set; }
}