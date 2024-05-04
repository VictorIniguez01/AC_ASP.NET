using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class UserAcRepository : IRepository<UserAc>
    {
        private ControlAccessContext _context;
        public UserAcRepository(ControlAccessContext context)
        {
            _context = context;
        }

        public async Task Add(UserAc entity)
            => await _context.UserAcs.AddAsync(entity);

        public void Delete(UserAc entity)
            => _context.UserAcs.Remove(entity);

        public async Task<IEnumerable<UserAc>> Get()
            => await _context.UserAcs.ToListAsync();

        public async Task<UserAc> GetById(int id)
            => await _context.UserAcs.FindAsync(id);

        public async Task Save()
         => await _context.SaveChangesAsync();

        public IEnumerable<UserAc> Search(Func<UserAc, bool> filter)
            => _context.UserAcs.Where(filter).ToList();

        public void Update(UserAc entity)
        {
            _context.UserAcs.Attach(entity);
            _context.UserAcs.Entry(entity).State = EntityState.Modified;
        }
    }
}
