using AutoMapper;
using Booky.ADL.Models;
using Booky.PL.ViewModels;

namespace Booky.PL.MappingProfile;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<ProductViewModel,Product>().ReverseMap();
    }
}