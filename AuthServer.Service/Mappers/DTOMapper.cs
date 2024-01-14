namespace AuthServer.Service.Mappers;
public class DTOMapper : Profile
{
    public DTOMapper()
    {
        CreateMap<ProductDTO, Product>().ReverseMap();
        CreateMap<UserAppDTO, UserApp>().ReverseMap();
    }
}