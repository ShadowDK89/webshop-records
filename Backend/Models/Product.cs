using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Product
{

    public Product()
    {
        this.Color = new HashSet<Color>();
        this.Genre = new HashSet<Genre>();
        this.Format = new HashSet<Format>();
    }

    public int Id { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Genre> Genre { get; set; }
    public int InStock { get; set; }
    public int Price { get; set; }
    public ICollection<Format> Format { get; set; } //CD or Vinyl
    public ICollection<Color> Color { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string ImgUrl { get; set; }
    public string SpotifyLink { get; set; }
    [JsonIgnore]
    public List<OrderProduct> OrderProducts { get; set; }
    public ICollection<TrackList> TrackList { get; set; }

    [JsonIgnore]
    public List<ProductColor> ProductColor { get; set; }
    [JsonIgnore]
    public List<ProductFormat> ProductFormat { get; set; }
    [JsonIgnore]
    public List<ProductGenre> ProductGenre { get; set; }
}