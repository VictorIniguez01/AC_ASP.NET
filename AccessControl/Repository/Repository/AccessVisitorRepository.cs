using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class AccessVisitorRepository : IRepository<AccessVisitor>
    {
        public ControlAccessContext _context;
        public AccessVisitorRepository(ControlAccessContext context)
        {
            _context = context;
        }

        public async Task Add(AccessVisitor entity)
            => await _context.AccessVisitors.AddAsync(entity);

        public void Delete(AccessVisitor entity)
            => _context.AccessVisitors.Remove(entity);

        public async Task<IEnumerable<AccessVisitor>> Get()
            => await _context.AccessVisitors.ToListAsync();

        public async Task<AccessVisitor> GetById(int id)
            => await _context.AccessVisitors.FindAsync(id);

        public Task Save()
            => _context.SaveChangesAsync();

        public IEnumerable<AccessVisitor> Search(Func<AccessVisitor, bool> filter)
            => _context.AccessVisitors.Where(filter).ToList();

        public void Update(AccessVisitor entity)
        {
            _context.AccessVisitors.Attach(entity);
            _context.AccessVisitors.Entry(entity).State = EntityState.Modified;
        }
    }
}
