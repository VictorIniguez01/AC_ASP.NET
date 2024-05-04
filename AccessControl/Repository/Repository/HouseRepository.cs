﻿using Repository.Models;
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
