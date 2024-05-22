using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BeEventy.Data.Models;
using BeEventy.Data.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeEventy.Data;
using System.Xml.Xsl;
using static BeEventy.Data.Models.Login;

namespace BeEventy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventRepository _eventRepository;
        private readonly AccountRepository _accountRepository;
        private readonly AppDbContext _context;

        public EventController(EventRepository eventRepository, AccountRepository accountRepository, AppDbContext context)
        {
            _eventRepository = eventRepository;
            _accountRepository = accountRepository;
            _context = context;
        }

        [HttpGet("getAllEvents")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            var events = await _eventRepository.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Event>> GetEventById(int id)
        {
            var ev = await _eventRepository.GetEventByIdAsync(id);
            if (ev == null)
            {
                return NotFound();
            }
            return Ok(ev);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Event>> GetEventByName(string name)
        {
            var ev = await _eventRepository.GetEventByNameAsync(name);
            if (ev == null)
            {
                return NotFound();
            }
            return Ok(ev);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> AddEvent(Event ev)
        {
            await _eventRepository.AddEventAsync(ev);
            return CreatedAtAction(nameof(GetAllEvents), new { id = ev.Id }, ev);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventById(int id)
        {
            await _eventRepository.DeleteEventAsync(id);
            return NoContent();
        }


        [HttpPost("{eventId}/plus")]
        public async Task<IActionResult> AddPlusToEvent(int eventId, [FromBody] LoginResponse loginResponse)
        {
            var loggedInUserId = loginResponse.UserId;
            var success = await _eventRepository.AddPlusToEventAsync(eventId, loggedInUserId);

            if (success)
            {
                return Ok("Plus został dodany do wydarzenia.");
            }
            else
            {
                return BadRequest("Nie udało się dodać plusa do wydarzenia.");
            }
        }
        [HttpPost("{eventId}/minus")]
        public async Task<IActionResult> AddPMinusToEvent(int eventId, [FromBody] LoginResponse loginResponse)
        {
            var loggedInUserId = loginResponse.UserId;
            var success = await _eventRepository.AddMinusToEventAsync(eventId, loggedInUserId);

            if (success)
            {
                return Ok("Minus został dodany do wydarzenia.");
            }
            else
            {
                return BadRequest("Nie udało się dodać minusa do wydarzenia.");
            }
        }
        [HttpGet("sort/date")]
        public async Task<ActionResult<IEnumerable<Event>>> SortEventsByDate()
        {
            var sortedEvents = await _eventRepository.SortEventsByDateAsync();
            return Ok(sortedEvents);
        }

        [HttpGet("sort/votes")]
        public async Task<ActionResult<IEnumerable<Event>>> SortEventsByVotes()
        {
            var sortedEvents = await _eventRepository.SortEventsByVotesAsync();
            return Ok(sortedEvents);
        }

    }
}
