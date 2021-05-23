using System.Collections.Generic;
using System.Text.Json.Serialization;

public class TrackList
{
    public int Id { get; set; }
    public string SongTitle { get; set; }
    public int TrackNumber { get; set; }

    public int ProductId { get; set; }
    [JsonIgnore]
    public Product Product { get; set; }
}