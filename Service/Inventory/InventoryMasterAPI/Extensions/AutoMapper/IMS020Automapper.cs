using AutoMapper;
using InventoryMasterPostgreSQLDB.Models;
using static InventoryMasterAPI.Models.IMS020;

namespace InventoryMasterAPI.Extensions.AutoMapper
{
    public class IMS020Automapper : Profile
    {
        public IMS020Automapper()
        {
            CreateMap<(TmLocation location, TmArea area), IMS020_Search_Result>()
                .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.location.LocationId))
                .ForMember(dest => dest.LocationCode, opt => opt.MapFrom(src => src.location.LocationCode))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.location.LocationName))
                .ForMember(dest => dest.AreaId, opt => opt.MapFrom(src => src.location.AreaId))
                .ForMember(dest => dest.AreaCode, opt => opt.MapFrom(src => src.area.AreaCode))
                .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.area.WarehouseId))
                .ForMember(dest => dest.RmFlag, opt => opt.MapFrom(src => src.location.RmFlag))
                .ForMember(dest => dest.FgFlag, opt => opt.MapFrom(src => src.location.FgFlag))
                .ForMember(dest => dest.ActiveFlag, opt => opt.MapFrom(src => src.location.ActiveFlag))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.location.CreatedDate))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.location.CreatedBy))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.location.UpdatedDate))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.location.UpdatedBy));

        }
    }
}
