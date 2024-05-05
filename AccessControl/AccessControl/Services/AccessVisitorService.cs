using AccessControl.DTOs;
using Repository.Models;
using Repository.Repository;
using AutoMapper;

namespace AccessControl.Services
{
    public class AccessVisitorService : ICommonService<AccessVisitorDto, AccessVisitorInsertDto>, IUpdateService<AccessVisitorDto, AccessVisitorUpdateDto>
    {
        private IRepository<AccessVisitor> _accessVisitorRepository;
        private IMapper _mapper;

        public List<string> Errors { get; }

        public AccessVisitorService(IRepository<AccessVisitor> accessVisitorRepository,
                                    IMapper mapper)
        {
            _accessVisitorRepository = accessVisitorRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<AccessVisitorDto>> Get()
        {
            IEnumerable<AccessVisitor> accessVisitors = await _accessVisitorRepository.Get();

            return accessVisitors.Select(av => _mapper.Map<AccessVisitorDto>(av));
        }

        public async Task<AccessVisitorDto> GetById(int id)
        {
            AccessVisitor accessVisitor = await _accessVisitorRepository.GetById(id);
            if (accessVisitor == null)
                return null;

            return _mapper.Map<AccessVisitorDto>(accessVisitor);
        }

        public async Task<AccessVisitorDto> Add(AccessVisitorInsertDto insertDto)
        {
            AccessVisitor accessVisitor = _mapper.Map<AccessVisitor>(insertDto);

            await _accessVisitorRepository.Add(accessVisitor);
            await _accessVisitorRepository.Save();

            AccessVisitorDto accessVisitorDto = _mapper.Map<AccessVisitorDto>(accessVisitor);
            accessVisitorDto.IsEntry = true;
            accessVisitorDto.IsGoingZone = true;

            return accessVisitorDto;
        }

        public async Task<AccessVisitorDto> Update(int id, AccessVisitorUpdateDto updateDto)
        {
            AccessVisitor accessVisitor = await _accessVisitorRepository.GetById(id);
            if (accessVisitor == null)
                return null;

            accessVisitor = _mapper.Map<AccessVisitorUpdateDto, AccessVisitor>(updateDto, accessVisitor);
            _accessVisitorRepository.Update(accessVisitor);
            await _accessVisitorRepository.Save();

            return _mapper.Map<AccessVisitorDto>(accessVisitor);
        }

        public async Task<AccessVisitorDto> Delete(int id)
        {
            AccessVisitor accessVisitor = await _accessVisitorRepository.GetById(id);
            if (accessVisitor == null)
                return null;

            AccessVisitorDto accessVisitorDto = _mapper.Map<AccessVisitorDto>(accessVisitor);

            _accessVisitorRepository.Delete(accessVisitor);
            await _accessVisitorRepository.Save();

            return accessVisitorDto;
        }

        public bool Validate(AccessVisitorInsertDto insertDto)
        {
            if (_accessVisitorRepository.Search(av => av.VisitorId == insertDto.VisitorId).Count() > 0)
            {
                Errors.Add("Existing visitor. The visitor is already inside");
                return false;
            }
            return true;
        }
    }
}
