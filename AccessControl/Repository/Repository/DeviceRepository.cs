using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class DeviceRepository : IRepository<Device>
    {
        public ControlAccessContext _context;
        public DeviceRepository(ControlAccessContext context)
        {
            _context = context;
        }

        public async Task Add(Device entity)
            => await _context.Devices.AddAsync(entity);

        public void Delete(Device entity)
            => _context.Devices.Remove(entity);

        public async Task<IEnumerable<Device>> Get()
            => await _context.Devices.ToListAsync();

        public async Task<Device> GetById(int id)
            => await _context.Devices.FindAsync(id);

        public async Task<IEnumerable<Device>> GetByUserId(int userAcId)
        {
            var query = _context.UserAcs
                        .Join(_context.Devices, u => u.UserAcId, d => d.UserAcId, (u, d) => new
                        {
                            UserAc = u,
                            Device = d
                        });

            return await query.Where(u => u.UserAc.UserAcId == userAcId).Select(d => d.Device).ToListAsync();
        }

        public Task Save()
            => _context.SaveChangesAsync();

        public IEnumerable<Device> Search(Func<Device, bool> filter)
            => _context.Devices.Where(filter).ToList();

        public void Update(Device entity)
        {
            _context.Devices.Attach(entity);
            _context.Devices.Entry(entity).State = EntityState.Modified;
        }
    }
}
