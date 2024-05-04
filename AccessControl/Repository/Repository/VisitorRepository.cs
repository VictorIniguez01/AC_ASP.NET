using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class VisitorRepository : IRepository<Visitor>
    {
        private ControlAccessContext _context;
        public VisitorRepository(ControlAccessContext context)
        {
            _context = context;
        }

        public async Task Add(Visitor entity)
            => await _context.Visitors.AddAsync(entity);

        public void Delete(Visitor entity)
            => _context.Visitors.Remove(entity);

        public async Task<IEnumerable<Visitor>> Get()
            => await _context.Visitors.ToListAsync();

        public async Task<Visitor> GetById(int id)
            => await _context.Visitors.FindAsync(id);

        public async Task Save()
         => await _context.SaveChangesAsync();

        public IEnumerable<Visitor> Search(Func<Visitor, bool> filter)
            => _context.Visitors.Where(filter).ToList();

        public void Update(Visitor entity)
        {
            _context.Visitors.Attach(entity);
            _context.Visitors.Entry(entity).State = EntityState.Modified;
        }
    }
}
