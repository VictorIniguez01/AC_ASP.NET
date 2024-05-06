using AccessControl.DTOs;
using AutoMapper;
using Repository.Models;
using Repository.Repository;

namespace AccessControl.Services
{
    public class ZoneService : IReadService<ZoneDto>
    {
        private IRepository<Zone> _zoneRepository;
        private IMapper _mapper;
        public ZoneService(IRepository<Zone> zoneRepository,
                           IMapper mapper)
        {
            _zoneRepository = zoneRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ZoneDto>> Get()
        {
            IEnumerable<Zone> zones = await _zoneRepository.Get();

            return zones.Select(z => _mapper.Map<ZoneDto>(z));
        }

        public async Task<ZoneDto> GetById(int id)
        {
            Zone zone = await _zoneRepository.GetById(id);
            if (zone == null)
                return null;

            return _mapper.Map<ZoneDto>(zone);
        }
    }
}
