using AccessControl.DTOs;
using Repository.Models;
using Repository.Repository;
using AutoMapper;

namespace AccessControl.Services
{
    public class VisitorService : ICommonService<VisitorDto, VisitorInsertDto, VisitorUpdateDto>
    {
        private IRepository<Visitor> _visitorRepository;
        private IMapper _mapper;
        public VisitorService(IRepository<Visitor> visitorRepository,
                              IMapper mapper)
        {
            _visitorRepository = visitorRepository;
            _mapper = mapper;
        }
        public async Task<VisitorDto> Add(VisitorInsertDto insertDto)
        {
            Visitor visitor = _mapper.Map<Visitor>(insertDto);

            await _visitorRepository.Add(visitor);
            await _visitorRepository.Save();

            return _mapper.Map<VisitorDto>(visitor);
        }

        public async Task<VisitorDto> Delete(int id)
        {
            Visitor visitor = await _visitorRepository.GetById(id);
            if(visitor == null)
                return null;

            VisitorDto visitorDto = _mapper.Map<VisitorDto>(visitor);

            _visitorRepository.Delete(visitor);
            await _visitorRepository.Save();

            return visitorDto;
        }

        public async Task<IEnumerable<VisitorDto>> Get()
        {
            IEnumerable<Visitor> visitors = await _visitorRepository.Get();

            return visitors.Select(v => _mapper.Map<VisitorDto>(v));
        }

        public async Task<VisitorDto> GetById(int id)
        {
            Visitor visitor = await _visitorRepository.GetById(id);
            if (visitor == null)
                return null;

            return _mapper.Map<VisitorDto>(visitor);
        }

        public async Task<VisitorDto> Update(int id, VisitorUpdateDto updateDto)
        {
            Visitor visitor = await _visitorRepository.GetById(id);
            if (visitor == null)
                return null;

            visitor = _mapper.Map<VisitorUpdateDto, Visitor>(updateDto, visitor);

            _visitorRepository.Update(visitor);
            await _visitorRepository.Save();

            return _mapper.Map<VisitorDto>(visitor);
        }
    }
}
