using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class CardRepository : IRepository<Card>
    {
        public ControlAccessContext _context;
        public CardRepository(ControlAccessContext context)
        {
            _context = context;
        }

        public async Task Add(Card entity)
            => await _context.Cards.AddAsync(entity);

        public void Delete(Card entity)
            => _context.Cards.Remove(entity);

        public async Task<IEnumerable<Card>> Get()
            => await _context.Cards.ToListAsync();

        public async Task<Card> GetById(int id)
            => await _context.Cards.FindAsync(id);

        public async Task<IEnumerable<Card>> GetByUserId(int userAcId)
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
                        .Join(_context.Residents, udzh => udzh.House.HouseId, v => v.HouseId, (udzh, v) => new
                        {
                            udzh.UserAc,
                            udzh.Device,
                            udzh.Zone,
                            udzh.House,
                            Resident = v
                        })
                        .Join(_context.Cards, udzhr => udzhr.Resident.ResidentId, c => c.ResidentId, (udzhr, c) => new
                        {
                            udzhr.UserAc,
                            udzhr.Device,
                            udzhr.Zone,
                            udzhr.House,
                            udzhr.Resident,
                            Card = c
                        });

            return await query.Where(u => u.UserAc.UserAcId == userAcId).Select(c => c.Card).ToListAsync();
        }

        public Task Save()
            => _context.SaveChangesAsync();

        public IEnumerable<Card> Search(Func<Card, bool> filter)
            => _context.Cards.Where(filter).ToList();

        public void Update(Card entity)
        {
            _context.Cards.Attach(entity);
            _context.Cards.Entry(entity).State = EntityState.Modified;
        }
    }
}
