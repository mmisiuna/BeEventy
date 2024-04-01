using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BeEventy.Data.Models;
using BeEventy.Data.Repositories;
using System.Threading.Tasks;

namespace BeEventy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventRepository _eventRepository;

        public EventController(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
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
    }
}
