using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class ResidentRepository : IRepository<Resident>
    {
        private ControlAccessContext _context;
        public ResidentRepository(ControlAccessContext context)
        {
            _context = context;
        }

        public async Task Add(Resident entity)
            => await _context.Residents.AddAsync(entity);

        public void Delete(Resident entity)
            => _context.Residents.Remove(entity);

        public async Task<IEnumerable<Resident>> Get()
            => await _context.Residents.ToListAsync();

        public async Task<Resident> GetById(int id)
            => await _context.Residents.FindAsync(id);

        public async Task Save()
         => await _context.SaveChangesAsync();

        public IEnumerable<Resident> Search(Func<Resident, bool> filter)
            => _context.Residents.Where(filter).ToList();

        public void Update(Resident entity)
        {
            _context.Residents.Attach(entity);
            _context.Residents.Entry(entity).State = EntityState.Modified;
        }
    }
}
