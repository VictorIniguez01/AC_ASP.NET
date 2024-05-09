using AccessControl.DTOs;
using Repository.Models;
using Repository.Repository;
using AutoMapper;

namespace AccessControl.Services
{
    public class VisitorService : ICreateService<VisitorDto, VisitorInsertDto>,
                                  IReadService<VisitorDto>,
                                  IDeleteService<VisitorDto>
    {
        private IRepository<Visitor> _visitorRepository;
        private IMapper _mapper;
        public List<string> Errors { get; }
        public VisitorService(IRepository<Visitor> visitorRepository,
                              IMapper mapper)
        {
            _visitorRepository = visitorRepository;
            _mapper = mapper;
            Errors = new List<string>();
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

        public async Task<IEnumerable<VisitorDto>> GetByUserId(int userAcId)
        {
            IEnumerable<Visitor> visitors = await _visitorRepository.GetByUserId(userAcId);

            return visitors.Select(v => _mapper.Map<VisitorDto>(v));
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

        public bool Validate(VisitorInsertDto insertDto)
        {
            if (_visitorRepository.Search(av => av.CarId == insertDto.CarId).Count() > 0)
            {
                Errors.Add("Existing car. The car is already inside");
                return false;
            }
            return true;
        }
    }
}
