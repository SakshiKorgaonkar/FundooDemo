using Microsoft.EntityFrameworkCore;
using RepoLayer.Context;
using RepoLayer.CustomException;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class CollaboratorRL : ICollaboratorRL
    {
        private readonly ProjectContext projectContext;

        public CollaboratorRL(ProjectContext projectContext)
        {
            this.projectContext = projectContext;
        }
        public string AddCollaborator(int noteId, string email)
        {
            var note=projectContext.Notes.FirstOrDefault(x=>x.Id==noteId);
            if (note == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            var existingCollaborator = projectContext.Collaborators
            .FirstOrDefault(c => c.NoteId == noteId && c.Email == email);
            if (existingCollaborator != null)
            {
                throw new CustomException1("Collaborator already exists");
            }

            var collab = new CollaboratorEntity { NoteId = noteId, Email = email };
            projectContext.Collaborators.Add(collab);
            projectContext.SaveChanges();
            return email;

        }

        public List<CollaboratorEntity> GetCollaboratorByNoteId(int noteId)
        {
            var note = projectContext.Notes
           .Include(n => n.Collaborators)
           .FirstOrDefault(n => n.Id == noteId);

            if (note == null)
            {
                throw new CustomException1("Note doesn't exist");
            }

            return note.Collaborators.ToList();
        }

        public string RemoveCollaborator(int noteId, string email)
        {
            var collaborator = projectContext.Collaborators
           .FirstOrDefault(c => c.NoteId == noteId && c.Email == email);

            if (collaborator == null)
            {
                throw new CustomException1("Collaborator doesn't exist");
            }

            projectContext.Collaborators.Remove(collaborator);
            projectContext.SaveChanges();
            return email;
        }
    }
}
