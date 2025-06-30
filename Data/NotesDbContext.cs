using Microsoft.EntityFrameworkCore;
using CloudNotes.Api.Models;

namespace CloudNotes.Api.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
    }
}