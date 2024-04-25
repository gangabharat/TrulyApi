using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using TrulyApi.Common;
using TrulyApi.Context;
using TrulyApi.Dtos;
using TrulyApi.Dtos.Quote;
using TrulyApi.Entities;
using TrulyApi.Enum;
using TrulyApi.Extensions;
using TrulyApi.Services;

namespace TrulyApi.Controllers
{
    public class QuotesController : ApiControllerBase
    {
        private readonly TrulyApiContext _context;
        private readonly ICsvFileBuilder _csvFileBuilder;
        public QuotesController(TrulyApiContext context, ICsvFileBuilder csvFileBuilder)
        {
            _context = context;
            _csvFileBuilder = csvFileBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<QuoteItem>>> GetQuoteItemsWithPagination([FromQuery] GetQuoteQuery query)
        {
            return await _context.Quotes.PaginatedListAsync(query.PageNumber, query.PageSize);
        }

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
                Created = DateTime.UtcNow
            };
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
                return NotFound();
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

        [HttpGet("export")]
        public async Task<ActionResult> Export()
        {
            var records = await _context.Quotes
                .Select(x => new ExportQuoteItemRecord
                {
                    Title = x.Title,
                    Author = x.Author,
                    Comments = x.Comments,
                    Note = x.Note,
                    Rating = x.Rating == null ? 0 : (int)x.Rating,
                    ReferenceUrl = x.ReferenceUrl,
                    Source = x.Source,
                    Tags = x.Tags
                })
                .ToListAsync();

            var vm = new ExportFileVm(
                $"{Guid.NewGuid()}.csv",
                "text/csv",
                //_csvFileBuilder.BuildFile(records)
                _csvFileBuilder.BuildQuoteItemsFile(records)
                );
            //return FilterContext()
            return File(vm.Content, vm.ContentType, $"CSV-{Guid.NewGuid()}.csv");
        }
    }
}
