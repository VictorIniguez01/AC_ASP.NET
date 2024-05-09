using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class ZoneRepository : IRepository<Zone>
    {
        private ControlAccessContext _context;
        public ZoneRepository(ControlAccessContext context) 
        { 
            _context = context;
        }
        
        public async Task Add(Zone entity)
            => await _context.Zones.AddAsync(entity);

        public void Delete(Zone entity)
            => _context.Zones.Remove(entity);

        public async Task<IEnumerable<Zone>> Get()
            => await _context.Zones.ToListAsync();

        public async Task<Zone> GetById(int id)
            => await _context.Zones.FindAsync(id);

        public async Task<IEnumerable<Zone>> GetByUserId(int userAcId)
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
                        });

            return await query.Where(u => u.UserAc.UserAcId == userAcId).Select(z => z.Zone).ToListAsync();
        }

        public async Task Save()
            => await _context.SaveChangesAsync();

        public IEnumerable<Zone> Search(Func<Zone, bool> filter)
            => _context.Zones.Where(filter).ToList();

        public void Update(Zone entity)
        {
            _context.Zones.Attach(entity);
            _context.Zones.Entry(entity).State = EntityState.Modified;
        }
    }
}
