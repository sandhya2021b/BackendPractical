using AutoMapper;
using IpInfo;
using GeoIPService.API.Services.Models;

namespace GeoIPService.API
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<FullResponse, GeoIPResponse>();
        }
    }
}
