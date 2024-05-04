using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class CarRepository : IRepository<Car>
    {
        public ControlAccessContext _context;
        public CarRepository(ControlAccessContext context)
        {
            _context = context;
        }

        public async Task Add(Car entity)
            => await _context.Cars.AddAsync(entity);

        public void Delete(Car entity)
            => _context.Cars.Remove(entity);

        public async Task<IEnumerable<Car>> Get()
            => await _context.Cars.ToListAsync();

        public async Task<Car> GetById(int id)
            => await _context.Cars.FindAsync(id);

        public Task Save()
            => _context.SaveChangesAsync();

        public IEnumerable<Car> Search(Func<Car, bool> filter)
            => _context.Cars.Where(filter).ToList();

        public void Update(Car entity)
        {
            _context.Cars.Attach(entity);
            _context.Cars.Entry(entity).State = EntityState.Modified;
        }
    }
}
