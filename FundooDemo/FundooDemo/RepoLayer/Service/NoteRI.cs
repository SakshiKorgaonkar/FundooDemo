using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ModelLayer;
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
    public class NoteRI : INoteRI
    {
        private readonly ProjectContext projectContext;

        public NoteRI(ProjectContext projectContext)
        {
            this.projectContext = projectContext;
        }

        public Note AddNote(NoteMI note)
        {
            Note note1 = new Note();
            note1.Title= note.Title;
            note1.Description= note.Description;
            note1.isArchived= note.isArchived;
            note1.isDeleted= note.isDeleted;
            projectContext.Notes.Add(note1);
            projectContext.SaveChanges();
            return note1;
        }

        public List<Note> GetAllNotes()
        {
            if (projectContext.Notes == null)
            {
                throw new CustomException1("No notes added");
            }
            return projectContext.Notes.ToList();
        }

        public Note GetNoteById(int id)
        {
            var note = projectContext.Notes.FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            return note;
        }

        public Note RemoveNote(int id)
        {
            var note = projectContext.Notes.FirstOrDefault(x => x.Id == id);
            if (note == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            projectContext.Notes.Remove(note);
            projectContext.SaveChanges() ;
            return note;
        }

        public Note UpdateNote(int id,NoteMI updatedNote)
        {
            var note = projectContext.Notes.FirstOrDefault(x => x.Id == id);
            if (note == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            note.Title = updatedNote.Title;
            note.Description = updatedNote.Description;
            note.isArchived=updatedNote.isArchived;
            note.isDeleted = updatedNote.isDeleted;
            projectContext.Notes.Update(note);
            projectContext.SaveChanges();
            return note;
        }
    }
}
