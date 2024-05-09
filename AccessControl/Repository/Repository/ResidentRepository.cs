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

        public async Task<IEnumerable<Resident>> GetByUserId(int userAcId)
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
                        .Join(_context.Residents, udzh => udzh.House.HouseId, r => r.HouseId, (udzh, r) => new
                        {
                            udzh.UserAc,
                            udzh.Device,
                            udzh.Zone,
                            udzh.House,
                            Resident = r
                        });

            return await query.Where(u => u.UserAc.UserAcId == userAcId).Select(r => r.Resident).ToListAsync();
        }

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
