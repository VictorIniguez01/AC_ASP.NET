using AccessControl.DTOs;
using AutoMapper;
using Repository.Models;
using Repository.Repository;

namespace AccessControl.Services
{
    public class HouseService : IReadService<HouseDto>
    {
        private IRepository<House> _houseRepository;
        private IMapper _mapper;
        public HouseService(IRepository<House> houseRepository,
                            IMapper mapper)
        {
            _houseRepository = houseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HouseDto>> Get()
        {
            IEnumerable<House> houses = await _houseRepository.Get();

            return houses.Select(h => _mapper.Map<HouseDto>(h));
        }

        public async Task<HouseDto> GetById(int id)
        {
            House house = await _houseRepository.GetById(id);
            if (house == null)
                return null;

            return _mapper.Map<HouseDto>(house);
        }
    }
}
