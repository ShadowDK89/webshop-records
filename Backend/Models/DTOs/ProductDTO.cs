using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ProductDTO
{
    public int Id { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Description { get; set; }
    public ICollection<GenreDTO> Genre { get; set; }
    public ICollection<ColorDTO> Color { get; set; }
    public int InStock { get; set; }
    public int Price { get; set; }
    public ICollection<FormatDTO> Format { get; set; } //CD or Vinyl
    public DateTime ReleaseDate { get; set; }
    public string ImgUrl { get; set; }
    public string SpotifyLink { get; set; }
    public ICollection<TrackListDTO> TrackList { get; set; }

}