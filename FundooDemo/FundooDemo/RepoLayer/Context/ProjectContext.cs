using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Context
{
    public class ProjectContext:DbContext
    {
        public ProjectContext(DbContextOptions options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<NoteEntity> Notes { get; set; }

        public DbSet<LabelEntity> Labels { get; set; }

        public DbSet<LabelNoteEntity> LabelNotes { get; set; }
        public DbSet<CollaboratorEntity> Collaborators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<LabelNoteEntity>()
                .HasKey(ln=>new {ln.NoteId,ln.LabelId});

            modelBuilder.Entity<LabelNoteEntity>()
                .HasOne(ln=>ln.Note)
                .WithMany(n=>n.LabelNotes)
                .HasForeignKey(ln=>ln.NoteId);

            modelBuilder.Entity<LabelNoteEntity>()
                .HasOne(ln=>ln.Label)
                .WithMany(l=>l.LabelNotes)
                .HasForeignKey(ln=>ln.LabelId);

            modelBuilder.Entity<CollaboratorEntity>()
                .HasIndex(c => new { c.Email, c.NoteId })
                .IsUnique();
        }
    }
}
