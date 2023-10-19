using Microsoft.EntityFrameworkCore;
using TrulyApi.Context;
using TrulyApi.Entities;
using TrulyApi.Repositorys.Interfaces;

namespace TrulyApi.Repositorys
{
    public class QuotesRepository : IQuotesRepository
    {
        private readonly TrulyApiContext _context;
        public QuotesRepository(TrulyApiContext context)
        {
            _context = context;
        }

        public async Task<QuoteItem> CreateAsync(QuoteItem item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetAsync(id) ?? throw new Exception($"{id} is not found.");
            _context.Quotes.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<QuoteItem>> GetAllAsync()
        {
            return await _context.Set<QuoteItem>().ToListAsync();
        }

        public async Task<QuoteItem?> GetAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _context.Quotes.FindAsync(id);
        }

        public async Task UpdateAsync(QuoteItem item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
