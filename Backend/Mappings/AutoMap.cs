using AutoMapper;
public class AutoMap : Profile
{
    public AutoMap()
    {
        CreateMap<Customer, CustomerDTO>();
        CreateMap<CustomerDTO, Customer>();

        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDTO, Order>();

        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>();

        CreateMap<Genre, GenreDTO>();
        CreateMap<GenreDTO, Genre>();

        CreateMap<Format, FormatDTO>();
        CreateMap<FormatDTO, Format>();

        CreateMap<Color, ColorDTO>();
        CreateMap<ColorDTO, Color>();

        CreateMap<TrackList, TrackListDTO>();
        CreateMap<TrackListDTO, TrackList>();


        CreateMap<OrderProduct, OrderProductDTO>();
        CreateMap<OrderProductDTO, OrderProduct>();

        CreateMap<ProductFormat, ProductFormatDTO>();
        CreateMap<ProductFormatDTO, ProductFormat>();

        CreateMap<ProductColor, ProductColorDTO>();
        CreateMap<ProductColorDTO, ProductColor>();

        CreateMap<ProductGenre, ProductGenreDTO>();
        CreateMap<ProductGenreDTO, ProductGenre>();
    }
}