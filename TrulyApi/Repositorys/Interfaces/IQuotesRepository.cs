using TrulyApi.Entities;

namespace TrulyApi.Repositorys.Interfaces
{
    public interface IQuotesRepository
    {
        Task<QuoteItem?> GetAsync(int? id);

        Task<List<QuoteItem>> GetAllAsync();

        Task<QuoteItem> CreateAsync(QuoteItem item);

        Task DeleteAsync(int id);

        Task UpdateAsync(QuoteItem item);
    }
}
