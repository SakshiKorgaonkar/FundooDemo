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
    }
}
