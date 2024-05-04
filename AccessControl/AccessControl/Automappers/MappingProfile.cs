using AccessControl.DTOs;
using Repository.Models;
using AutoMapper;

namespace AccessControl.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //AccessResident
            CreateMap<AccessResidentInsertDto, AccessResident>();
            CreateMap<AccessResident, AccessResidentDto>();
            CreateMap<AccessResidentUpdateDto, AccessResident>();

            //AccessVisitor
            CreateMap<AccessVisitorInsertDto, AccessVisitor>();
            CreateMap<AccessVisitor, AccessVisitorDto>();
            CreateMap<AccessVisitorUpdateDto, AccessVisitor>();

            //Car
            CreateMap<CarInsertDto, Car>();
            CreateMap<Car, CarDto>();
            CreateMap<CarUpdateDto, Car>();

            //Visitor
            CreateMap<VisitorInsertDto, Visitor>();
            CreateMap<Visitor, VisitorDto>();
            CreateMap<VisitorUpdateDto, Visitor>();
        }
    }
}
