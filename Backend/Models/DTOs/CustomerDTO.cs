using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class CustomerDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Adress { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    [JsonIgnore]
    public ICollection<OrderDTO> Orders { get; set; }
}