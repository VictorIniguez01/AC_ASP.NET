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
