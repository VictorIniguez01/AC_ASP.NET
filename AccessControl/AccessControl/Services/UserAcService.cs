using AccessControl.DTOs;
using AutoMapper;
using Repository.Models;
using Repository.Repository;

namespace AccessControl.Services
{
    public class UserAcService : ISessionService<UserAcDto>
    {
        private IRepository<UserAc> _userAcRepository;
        private IMapper _mapper;

        public UserAcService(IRepository<UserAc> userAcRepository,
                             IMapper mapper)
        {
            _userAcRepository = userAcRepository;
            _mapper = mapper;
        }

        public async Task<UserAcDto> Auth(UserAcDto userAcDto)
        {
            IEnumerable<UserAc> user = _userAcRepository.Search(u => u.UserAcName == userAcDto.UserAcName
                                                                && u.UserAcPassword == userAcDto.UserAcPassword);
            if (user.Count() > 0)
                return _mapper.Map<UserAcDto>(user.First());

            return null;
        }
    }
}
