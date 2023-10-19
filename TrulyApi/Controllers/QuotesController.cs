using Microsoft.AspNetCore.Mvc;
using TrulyApi.Common;
using TrulyApi.Context;
using TrulyApi.Dtos.Quote;
using TrulyApi.Entities;
using TrulyApi.Enum;
using TrulyApi.Extensions;

namespace TrulyApi.Controllers
{    
    public class QuotesController : ApiControllerBase
    {
        private readonly TrulyApiContext _context;
        public QuotesController(TrulyApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<QuoteItem>>> GetQuoteItemsWithPagination([FromQuery] GetQuoteQuery query)
        {
            return await _context.Quotes.PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        // GET: api/QuoteItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuoteItem>> GetQuoteItem(int id)
        {

            var quoteItem = await _context.Quotes.FindAsync(id);

            if (quoteItem == null)
            {
                return NotFound();
            }

            return quoteItem;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateQuoteItemCommand request)
        {
            var entity = new QuoteItem
            {
                Author = request.Author,
                Title = request.Title,
                Note = request.Note,
                Comments = request.Comments,
                Source = request.Source,
                Rating = (RatingLevel)request.Rating,
                ReferenceUrl = request.ReferenceUrl,
                Tags = request.Tags,
                Created = DateTime.UtcNow,
                //CreatedBy = "user",
                //LastModified = DateTime.UtcNow,
                //LastModifiedBy ="user"

            };

            //entity.AddDomainEvent(new QuoteItemCreatedEvent(entity));
            _context.Quotes.Add(entity);

            return await _context.SaveChangesAsync();


        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateQuoteListCommand request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var entity = await _context.Quotes
             .FindAsync(new object[] { request.Id });

            if (entity == null)
            {
                throw new Exception();
            }

            entity.Title = request.Title;
            entity.Author = request.Author;
            entity.Title = request.Title;
            entity.Note = request.Note;
            entity.Comments = request.Comments;
            entity.Source = request.Source;
            entity.Rating = (RatingLevel)request.Rating;
            entity.ReferenceUrl = request.ReferenceUrl;
            entity.Tags = request.Tags;
            entity.LastModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();


            return NoContent();
        }

        // DELETE: api/QuoteItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuoteItem(int id)
        {

            var quoteItem = await _context.Quotes.FindAsync(id);
            if (quoteItem == null)
            {
                return NotFound();
            }

            _context.Quotes.Remove(quoteItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
