
using AccessControl.DTOs;
using AutoMapper;
using Repository.Models;
using Repository.Repository;

namespace AccessControl.Services
{
    public class AccessResidentService : ICommonService<AccessResidentDto, AccessResidentInsertDto, AccessResidentUpdateDto>
    {
        private IRepository<AccessResident> _accessResidentRepository;
        private IMapper _mapper;
        public AccessResidentService(IRepository<AccessResident> accessResidentRepository,
                                     IMapper mapper)
        {
            _accessResidentRepository = accessResidentRepository;
            _mapper = mapper;
        }

        public async Task<AccessResidentDto> Add(AccessResidentInsertDto insertDto)
        {
            AccessResident accessResident = _mapper.Map<AccessResident>(insertDto);

            await _accessResidentRepository.Add(accessResident);
            await _accessResidentRepository.Save();

            return _mapper.Map<AccessResidentDto>(accessResident);
        }

        public async Task<AccessResidentDto> Delete(int id)
        {
            AccessResident accessResident = await _accessResidentRepository.GetById(id);
            if (accessResident == null)
                return null;

            AccessResidentDto accessResidentDto = _mapper.Map<AccessResidentDto>(accessResident);

            _accessResidentRepository.Delete(accessResident);
            await _accessResidentRepository.Save();

            return accessResidentDto;
        }

        public async Task<IEnumerable<AccessResidentDto>> Get()
        {
            IEnumerable<AccessResident> accessResidents = await _accessResidentRepository.Get();

            return accessResidents.Select(ar => _mapper.Map<AccessResidentDto>(ar));
        }

        public async Task<AccessResidentDto> GetById(int id)
        {
            AccessResident accessResident = await _accessResidentRepository.GetById(id);
            if (accessResident == null)
                return null;

            return _mapper.Map<AccessResidentDto>(accessResident);
        }

        public async Task<AccessResidentDto> Update(int id, AccessResidentUpdateDto updateDto)
        {
            AccessResident accessResident = await _accessResidentRepository.GetById(id);
            if (accessResident == null)
                return null;

            accessResident = _mapper.Map<AccessResidentUpdateDto, AccessResident>(updateDto, accessResident);
            _accessResidentRepository.Update(accessResident);
            await _accessResidentRepository.Save();

            return _mapper.Map<AccessResidentDto>(accessResident);
        }
    }
}
