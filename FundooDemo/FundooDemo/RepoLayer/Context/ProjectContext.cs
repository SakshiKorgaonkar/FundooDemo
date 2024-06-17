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

        public DbSet<Note> Notes { get; set; }

        public DbSet<Label> Labels { get; set; }

        public DbSet<LabelNote> LabelNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<LabelNote>().HasKey(ln=>new {ln.NoteId,ln.LabelId});

            modelBuilder.Entity<LabelNote>().HasOne(ln=>ln.Note).WithMany(n=>n.LabelNotes).HasForeignKey(ln=>ln.NoteId);

            modelBuilder.Entity<LabelNote>().HasOne(ln=>ln.Label).WithMany(l=>l.LabelNotes).HasForeignKey(ln=>ln.LabelId);
        }
    }
}
