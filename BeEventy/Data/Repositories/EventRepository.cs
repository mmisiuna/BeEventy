using System;
using System.Collections.Generic;
using System.Linq;
using BeEventy.Data.Models;
using Microsoft.EntityFrameworkCore;
using PostgreSQL.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static BeEventy.Data.Models.Login;

namespace BeEventy.Data.Repositories
{
    public class EventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Event> GetEventByNameAsync(string name)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Name == name);
        }

        public async Task AddEventAsync(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev != null)
            {
                _context.Events.Remove(ev);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UserHasVotedForEventAsync(int userId, int eventId)
        {
            return await _context.Votes
                .AnyAsync(uev => uev.UserId == userId && uev.EventId == eventId);
        }


        public async Task<bool> AddVoteToEventAsync(int eventId, int userId, bool isPlus)
        {
            // Sprawdź, czy istnieje wydarzenie o podanym identyfikatorze
            var eventExists = await _context.Events.FindAsync(eventId);
            if (eventExists == null)
            {
                return false; // Wydarzenie nie istnieje
            }

            // Sprawdź, czy użytkownik już oddał głos na to wydarzenie
            var userVote = await _context.Votes.FirstOrDefaultAsync(v => v.UserId == userId && v.EventId == eventId);
            if (userVote != null)
            {
                if (isPlus && userVote.IsPlus)
                {
                    return false; // Użytkownik już oddał plusa dla tego wydarzenia
                }
                else if (!isPlus && !userVote.IsPlus)
                {
                    return false; // Użytkownik już oddał minusa dla tego wydarzenia
                }
                else
                {
                    // Użytkownik zmienia swój głos, więc zmniejsz odpowiednio plusy lub minusy
                    if (isPlus)
                    {
                        eventExists.Pluses++;
                        eventExists.Minuses--;
                    }
                    else
                    {
                        eventExists.Pluses--;
                        eventExists.Minuses++;
                    }

                    userVote.IsPlus = isPlus; // Aktualizujemy informację o głosie w bazie danych
                }
            }
            else
            {
                // Tworzymy nowy głos
                var vote = new Vote
                {
                    UserId = userId,
                    EventId = eventId,
                    IsPlus = isPlus
                };

                // Dodajemy głos do bazy danych
                _context.Votes.Add(vote);

                // Zwiększamy odpowiednio liczbę plusów lub minusów dla wydarzenia
                if (isPlus)
                {
                    eventExists.Pluses++;
                }
                else
                {
                    eventExists.Minuses++;
                }
            }

            try
            {
                // Zapisujemy zmiany w bazie danych
                await _context.SaveChangesAsync();
                return true; // Pomyślnie dodano głos do wydarzenia
            }
            catch (Exception ex)
            {
                // Obsługa błędu zapisu
                return false; // Wystąpił błąd podczas dodawania głosu do wydarzenia
            }
        }

        public async Task<bool> AddPlusToEventAsync(int eventId, int userId)
        {
            return await AddVoteToEventAsync(eventId, userId, true);
        }

        public async Task<bool> AddMinusToEventAsync(int eventId, int userId)
        {
            return await AddVoteToEventAsync(eventId, userId, false);
        }

        public async Task<List<Event>> SortEventsByDateAsync()
        {
            var events = await _context.Events.OrderBy(e => e.DateOfStart).ToListAsync();
            return events;
        }

        public async Task<List<Event>> SortEventsByVotesAsync()
        {
            var events = await _context.Events
                .OrderByDescending(e => e.Pluses)
                .ThenByDescending(e => e.Minuses)
                .ToListAsync();
            return events;
        }

    }
}
