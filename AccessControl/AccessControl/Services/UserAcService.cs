using AccessControl.DTOs;
using AutoMapper;
using Repository.Models;
using Repository.Repository;

namespace AccessControl.Services
{
    public class UserAcService : ILoginService<UserAcDto>, IUserAcService<DeviceDto, VisitorDto, CarDto, AccessVisitorDto, AccessDetailsDto>
    {
        private IRepository<UserAc> _userAcRepository;
        private IMapper _mapper;
        private IUserRepository<Device, Visitor, Car, AccessVisitor> _userMethodsRepository;

        public UserAcService(IRepository<UserAc> userAcRepository,
                             IMapper mapper,
                             IUserRepository<Device, Visitor, Car, AccessVisitor> userMethodsRepository)
        {
            _userAcRepository = userAcRepository;
            _mapper = mapper;
            _userMethodsRepository = userMethodsRepository;
        }

        public async Task<UserAcDto> Auth(UserAcDto userAcDto)
        {
            IEnumerable<UserAc> user = _userAcRepository.Search(u => u.UserAcName == userAcDto.UserAcName
                                                                && u.UserAcPassword == userAcDto.UserAcPassword);
            if (user.Count() > 0)
                return _mapper.Map<UserAcDto>(user.First());

            return null;
        }

        public async Task<IEnumerable<AccessDetailsDto>> GetAccessDetails(int userAcId)
        {
            IEnumerable<AccessVisitorDto> accessVisitors = await GetAccessVisitor(userAcId);
            IEnumerable<VisitorDto> visitors = await GetVisitors(userAcId);
            IEnumerable<CarDto> cars = await GetCars(userAcId);

            IEnumerable<AccessDetailsDto> accessDetails = accessVisitors
                                .Join(visitors, ac => ac.VisitorId, v => v.VisitorId, (ac, v) => new
                                {
                                    AccessVisitorDto = ac,
                                    VisitorDto = v
                                })
                                .Join(cars, acv => acv.VisitorDto.CarId, c => c.CarId, (acv, c) => new AccessDetailsDto
                                {
                                    Visitor = acv.VisitorDto,
                                    AccessVisitor = acv.AccessVisitorDto,
                                    Car = c
                                });

            return accessDetails;
        }

        public async Task<IEnumerable<AccessVisitorDto>> GetAccessVisitor(int userAcId)
        {
            IEnumerable<AccessVisitor> accessVisitors = await _userMethodsRepository.GetAccessVisitor(userAcId);

            return accessVisitors.Select(ac => _mapper.Map<AccessVisitorDto>(ac));
        }

        public async Task<IEnumerable<CarDto>> GetCars(int userAcId)
        {
            IEnumerable<Car> cars = await _userMethodsRepository.GetCars(userAcId);

            return cars.Select(c => _mapper.Map<CarDto>(c));
        }

        public async Task<IEnumerable<DeviceDto>> GetDevices(int userAcId)
        {
            IEnumerable<Device> devices = await _userMethodsRepository.GetDevices(userAcId);

            return devices.Select(d => _mapper.Map<DeviceDto>(d));
        }

        public async Task<IEnumerable<VisitorDto>> GetVisitors(int userAcId)
        {
            IEnumerable<Visitor> visitors = await _userMethodsRepository.GetVisitors(userAcId);

            return visitors.Select(v => _mapper.Map<VisitorDto>(v));
        }
    }
}
