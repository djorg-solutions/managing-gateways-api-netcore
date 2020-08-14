using AutoMapper;
using GatewaysDomain.Models;
using ManagingGateways.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagingGateways.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Gateway, GatewayViewModel>()
                  .ForMember(m => m.GatewayId, opt => opt.MapFrom(src => src.Id))
                  .ForMember(m => m.GatewaySerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
                  .ForMember(m => m.GatewayHumanReadable, opt => opt.MapFrom(src => src.HumanReadable))
                  .ForMember(m => m.GatewayIpAddress, opt => opt.MapFrom(src => src.IpAddress))
                  .ForMember(m => m.GatewayDevices, opt => opt.MapFrom(src => src.Devices)).ReverseMap();

            CreateMap<Device, DeviceViewModel>()
                  .ForMember(m => m.DeviceId, opt => opt.MapFrom(src => src.Id))
                  .ForMember(m => m.DeviceUID, opt => opt.MapFrom(src => src.UID))
                  .ForMember(m => m.DeviceVendor, opt => opt.MapFrom(src => src.Vendor))
                  .ForMember(m => m.DeviceCreateAt, opt => opt.MapFrom(src => src.CreateAt))
                  .ForMember(m => m.DeviceStatus, opt => opt.MapFrom(src => src.Status))
                  .ForMember(m => m.DeviceGatewayId, opt => opt.MapFrom(src => src.Gateway.Id)).ReverseMap();
        }
    }
}
