using Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.Windows.Markup;

namespace Repository.Repository
{
    public class UserAcRepository : IRepository<UserAc>, IUserRepository<Device, Visitor, Car, AccessVisitor>
    {
        private ControlAccessContext _context;
        public UserAcRepository(ControlAccessContext context)
        {
            _context = context;
        }

        public async Task Add(UserAc entity)
            => await _context.UserAcs.AddAsync(entity);

        public void Delete(UserAc entity)
            => _context.UserAcs.Remove(entity);

        public async Task<IEnumerable<UserAc>> Get()
            => await _context.UserAcs.ToListAsync();

        public async Task<UserAc> GetById(int id)
            => await _context.UserAcs.FindAsync(id);

        public async Task Save()
         => await _context.SaveChangesAsync();

        public IEnumerable<UserAc> Search(Func<UserAc, bool> filter)
            => _context.UserAcs.Where(filter).ToList();

        public void Update(UserAc entity)
        {
            _context.UserAcs.Attach(entity);
            _context.UserAcs.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IEnumerable<AccessVisitor>> GetAccessVisitor(int userAcId)
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

        public async Task<IEnumerable<Car>> GetCars(int userAcId)
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

        public async Task<IEnumerable<Device>> GetDevices(int userAcId)
        {
            var query = _context.UserAcs
                        .Join(_context.Devices, u => u.UserAcId, d => d.UserAcId, (u, d) => new
                        {
                            UserAc = u,
                            Device = d
                        });

            return await query.Where(u => u.UserAc.UserAcId == userAcId).Select(d => d.Device).ToListAsync();
        }

        public async Task<IEnumerable<Visitor>> GetVisitors(int userAcId)
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
    }
}
