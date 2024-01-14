using AutoMapper;
using Booky.ADL.Models;
using Booky.PL.ViewModels;

namespace Booky.PL.MappingProfile;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UsersViews>().ReverseMap();
    }
}