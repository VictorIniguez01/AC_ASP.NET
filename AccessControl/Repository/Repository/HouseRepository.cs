using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class HouseRepository : IRepository<House>
    {
        public ControlAccessContext _context;
        public HouseRepository(ControlAccessContext context)
        {
            _context = context;
        }

        public async Task Add(House entity)
            => await _context.Houses.AddAsync(entity);

        public void Delete(House entity)
            => _context.Houses.Remove(entity);

        public async Task<IEnumerable<House>> Get()
            => await _context.Houses.ToListAsync();

        public async Task<House> GetById(int id)
            => await _context.Houses.FindAsync(id);

        public async Task<IEnumerable<House>> GetByUserId(int userAcId)
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
                        });

            return await query.Where(u => u.UserAc.UserAcId == userAcId).Select(h => h.House).ToListAsync();
        }

        public Task Save()
            => _context.SaveChangesAsync();

        public IEnumerable<House> Search(Func<House, bool> filter)
            => _context.Houses.Where(filter).ToList();

        public void Update(House entity)
        {
            _context.Houses.Attach(entity);
            _context.Houses.Entry(entity).State = EntityState.Modified;
        }
    }
}
