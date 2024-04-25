using Microsoft.AspNetCore.Mvc;
using TrulyApi.Common;
using TrulyApi.Context;
using TrulyApi.Dtos;
using TrulyApi.Entities;
using TrulyApi.Services;
using TrulyApi.Extensions;
using Microsoft.EntityFrameworkCore;
using TrulyApi.Dtos.Card;

namespace TrulyApi.Controllers
{
    public class CardController : ApiControllerBase
    {
        private readonly TrulyApiContext _context;
        private readonly ICsvFileBuilder _csvFileBuilder;
        public CardController(TrulyApiContext context, ICsvFileBuilder csvFileBuilder)
        {
            _context = context;
            _csvFileBuilder = csvFileBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<CardItem>>> GetQuoteItemsWithPagination([FromQuery] GetCardQuery query)
        {
            return await _context.Cards.PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardItem>> GetQuoteItem(int id)
        {

            var quoteItem = await _context.Cards.FindAsync(id);

            if (quoteItem == null)
            {
                return NotFound();
            }

            return quoteItem;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(SetCardItemCommand request)
        {
            var entity = new CardItem
            {
                Bank = request.Bank,
                Name = request.Name,
                Number = request.Number,
                CVV = request.CVV,
                Key = request.Key,
                Month = request.Month,
                Notes = request.Notes,
                Tagline = request.Tagline,
                Type = request.Type,
                Year = request.Year,

                Created = DateTime.UtcNow
            };
            _context.Cards.Add(entity);

            return await _context.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, SetCardItemCommand request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var entity = await _context.Cards
             .FindAsync(new object[] { request.Id });

            if (entity == null)
            {
                return NotFound();
            }

            entity.Bank = request.Bank;
            entity.Name = request.Name;
            entity.Number = request.Number;
            entity.CVV = request.CVV;
            entity.Key = request.Key;
            entity.Month = request.Month;
            entity.Notes = request.Notes;
            entity.Tagline = request.Tagline;
            entity.Type = request.Type;
            entity.Year = request.Year;

            entity.LastModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuoteItem(int id)
        {

            var item = await _context.Cards.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("export")]
        public async Task<ActionResult> Export()
        {
            var records = await _context.Cards
                .Select(x => new ExportCardItemRecord
                {
                    Bank = x.Bank,
                    Name = x.Name,
                    Number = x.Number,
                    CVV = x.CVV,
                    Key = x.Key,
                    Month = x.Month,
                    Notes = x.Notes,
                    Tagline = x.Tagline,
                    Type = x.Type,
                    Year = x.Year,
                })
                .ToListAsync();

            var vm = new ExportFileVm(
                $"{Guid.NewGuid()}.csv",
                "text/csv",
                //_csvFileBuilder.BuildFile(records)
                _csvFileBuilder.BuilCardItemsFile(records)
                );

            return File(vm.Content, vm.ContentType, vm.FileName);
        }
    }
}
