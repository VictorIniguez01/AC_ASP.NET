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

        public async Task<IEnumerable<AccessVisitor>> GetByUserId(int userAcId)
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
                        .Join(_context.AccessVisitors, udzhv => udzhv.Visitor.VisitorId, ac => ac.VisitorId, (udzhv, ac) => new
                        {
                            udzhv.UserAc,
                            udzhv.Device,
                            udzhv.Zone,
                            udzhv.House,
                            udzhv.Visitor,
                            AccessVisitor = ac
                        });

            return await query.Where(u => u.UserAc.UserAcId == userAcId).Select(ac => ac.AccessVisitor).ToListAsync();
        }

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
