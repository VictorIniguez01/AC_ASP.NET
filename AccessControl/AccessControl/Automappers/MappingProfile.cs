using AccessControl.DTOs;
using Repository.Models;
using AutoMapper;

namespace AccessControl.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //AccessVisitor
            CreateMap<AccessVisitorInsertDto, AccessVisitor>();
            CreateMap<AccessVisitor, AccessVisitorDto>();
            CreateMap<AccessVisitorUpdateDto, AccessVisitor>();

            //Car
            CreateMap<CarInsertDto, Car>();
            CreateMap<Car, CarDto>();

            //Visitor
            CreateMap<VisitorInsertDto, Visitor>();
            CreateMap<Visitor, VisitorDto>();

            //UserAc
            CreateMap<UserAc, UserAcDto>();

            //Device
            CreateMap<Device, DeviceDto>();
        }
    }
}
