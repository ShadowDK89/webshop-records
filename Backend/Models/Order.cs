using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Order
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string PaymentMethod { get; set; }
    public int TotalPrice { get; set; }

    public int CustomerId { get; set; }
    [JsonIgnore]
    public Customer Customer { get; set; }
    [JsonIgnore]
    public List<OrderProduct> OrderProducts { get; set; }
}