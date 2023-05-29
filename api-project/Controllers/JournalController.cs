using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JournalController : ControllerBase
    {
        private readonly DataContext context;

        public JournalController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Journal>>> Get() 
        {
            return Ok(await this.context.JournalEntries.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Journal>> Get(int id)
        {
            var entry = await this.context.JournalEntries.FindAsync(id);
            if (entry == null)
                return BadRequest("Entry not found");
            return Ok(entry);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Journal>>> AddEntry(Journal entry)
        {
            this.context.JournalEntries.Add(entry);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.JournalEntries.ToListAsync());
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Journal>>> UpdateEntry(Journal request)
        {
            var dbEntry = await this.context.JournalEntries.FindAsync(request.Id);
            if (dbEntry == null)
            {
                return BadRequest("Entry not found");
            }

            dbEntry.Name = request.Name;
            dbEntry.Title = request.Title;
            dbEntry.Description = request.Description;
            dbEntry.Place = request.Place;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.JournalEntries.ToListAsync());
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Journal>>> Delete(int id)
        {
            var dbEntry = await this.context.JournalEntries.FindAsync(id);
            if (dbEntry == null)
            {
                return BadRequest("Entry not found");
            }

            this.context.JournalEntries.Remove(dbEntry);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.JournalEntries.ToListAsync());
        }
    }
}
