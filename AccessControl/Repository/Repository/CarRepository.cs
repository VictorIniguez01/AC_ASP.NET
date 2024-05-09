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

        public async Task<IEnumerable<Car>> GetByUserId(int userAcId)
        {
            var query = _context.UserAcs
                        .Join(_context.Devices, u => u.UserAcId, d => d.UserAcId, (u, d) => new
                        {
                            UserAc = u,
                            Device = d
                        })
                        .Join(_context.Zones, ud => ud.Device.ZoneId, z => z.ZoneId, (ud, z) => new
                        {
                            ud.UserAc,
                            ud.Device,
                            Zone = z
                        })
                        .Join(_context.Houses, udz => udz.Zone.ZoneId, h => h.ZoneId, (udz, h) => new
                        {
                            udz.UserAc,
                            udz.Device,
                            udz.Zone,
                            House = h
                        })
                        .Join(_context.Visitors, udzh => udzh.House.HouseId, v => v.HouseId, (udzh, v) => new
                        {
                            udzh.UserAc,
                            udzh.Device,
                            udzh.Zone,
                            udzh.House,
                            Visitor = v
                        })
                        .Join(_context.Cars, udzhv => udzhv.Visitor.CarId, c => c.CarId, (udzhv, c) => new
                        {
                            udzhv.UserAc,
                            udzhv.Device,
                            udzhv.Zone,
                            udzhv.House,
                            udzhv.Visitor,
                            Car = c
                        });

            return await query.Where(u => u.UserAc.UserAcId == userAcId).Select(c => c.Car).ToListAsync();
        }

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
