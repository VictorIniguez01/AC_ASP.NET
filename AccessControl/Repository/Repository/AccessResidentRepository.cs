using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class AccessResidentRepository : IRepository<AccessResident>
    {
        public ControlAccessContext _context;
        public AccessResidentRepository(ControlAccessContext context)
        {
            _context = context;
        }

        public async Task Add(AccessResident entity)
            => await _context.AccessResidents.AddAsync(entity);

        public void Delete(AccessResident entity)
            => _context.AccessResidents.Remove(entity);

        public async Task<IEnumerable<AccessResident>> Get()
            => await _context.AccessResidents.ToListAsync();

        public async Task<AccessResident> GetById(int id)
            => await _context.AccessResidents.FindAsync(id);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public IEnumerable<AccessResident> Search(Func<AccessResident, bool> filter)
            => _context.AccessResidents.Where(filter).ToList();

        public void Update(AccessResident entity)
        {
            _context.AccessResidents.Attach(entity);
            _context.AccessResidents.Entry(entity).State = EntityState.Modified;
        }
    }
}
