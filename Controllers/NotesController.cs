using Microsoft.AspNetCore.Mvc;
using CloudNotes.Api.Models;

namespace CloudNotes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private static List<Note> _notes = new List<Note>();
        private static int _nextId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<Note>> Get() => Ok(_notes);

        [HttpGet("{id}")]
        public ActionResult<Note> Get(int id)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);
            if (note == null) return NotFound();
            return Ok(note);
        }

        [HttpPost]
        public ActionResult<Note> Post(Note note)
        {
            note.Id = _nextId++;
            note.CreatedAt = DateTime.UtcNow;
            _notes.Add(note);
            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Note updatedNote)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);
            if (note == null) return NotFound();

            note.Title = updatedNote.Title;
            note.Content = updatedNote.Content;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);
            if (note == null) return NotFound();

            _notes.Remove(note);
            return NoContent();
        }
    }
}