using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private static List<Journal> journal = new List<Journal>
            {
               new Journal {
                    Id = 1,
                    Name = "Test",
                    Title = "Test",
                    Description = "Test",
                    Place = "Test",
                },

               new Journal {
                    Id = 2,
                    Name = "Cora Rose Marlow",
                    Title = "Truth",
                    Description = "The One",
                    Place = "My Heart",
                },

               new Journal {
                    Id = 3,
                    Name = "Who",
                    Title = "Why",
                    Description = "What",
                    Place = "How",
                }
            };

        [HttpGet]
        public async Task<ActionResult<List<Journal>>> Get() 
        {
            return Ok(journal);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Journal>> Get(int id)
        {
            var entry = journal.Find(h => h.Id == id);
            if (entry == null)
                return BadRequest("Entry not found");
            return Ok(entry);
        }

        [HttpPost]
        public async Task<ActionResult<List<Journal>>> AddEntry(Journal entry)
        {
            journal.Add(entry);
            return Ok(journal);
        }

        [HttpPut]
        public async Task<ActionResult<List<Journal>>> UpdateEntry(Journal request)
        {
            var entry = journal.Find(h => h.Id == request.Id);
            if (entry == null)
            {
                return BadRequest("Entry not found");
            }

            entry.Name = request.Name;
            entry.Title = request.Title;
            entry.Description = request.Description;
            entry.Place = request.Place;

            return Ok(journal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Journal>>> Delete(int id)
        {
            var entry = journal.Find(h => h.Id == id);
            if (entry == null)
            {
                return BadRequest("Entry not found");
            }  

            journal.Remove(entry);
            return Ok(journal);
        }
    }
}
