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

        public async Task<IEnumerable<Visitor>> GetByUserId(int userAcId)
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
                        });

            return await query.Where(u => u.UserAc.UserAcId == userAcId).Select(v => v.Visitor).ToListAsync();
        }

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
