using Microsoft.AspNetCore.Mvc;
using CloudNotes.Api.Models;
using CloudNotes.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudNotes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesDbContext _context;

        public NotesController(NotesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> Get()
        {
            return await _context.Notes.OrderByDescending(n => n.CreatedAt).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> Get(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) return NotFound();
            return note;
        }

        [HttpPost]
        public async Task<ActionResult<Note>> Post(Note note)
        {
            note.CreatedAt = DateTime.UtcNow;
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Note updatedNote)
        {
            if (id != updatedNote.Id) return BadRequest();

            var note = await _context.Notes.FindAsync(id);
            if (note == null) return NotFound();

            note.Title = updatedNote.Title;
            note.Content = updatedNote.Content;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) return NotFound();

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}