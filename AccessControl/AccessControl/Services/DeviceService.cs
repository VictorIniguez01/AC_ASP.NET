using AccessControl.DTOs;
using AutoMapper;
using Repository.Models;
using Repository.Repository;

namespace AccessControl.Services
{
    public class DeviceService : IReadService<DeviceDto>
    {
        private IRepository<Device> _deviceRepository;
        private IMapper _mapper;
        public DeviceService(IRepository<Device> deviceRepository,
                             IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeviceDto>> Get()
        {
            IEnumerable<Device> devices = await _deviceRepository.Get();

            return devices.Select(d => _mapper.Map<DeviceDto>(d));
        }

        public async Task<DeviceDto> GetById(int id)
        {
            Device device = await _deviceRepository.GetById(id);
            if (device == null)
                return null;

            return _mapper.Map<DeviceDto>(device);
        }
    }
}
