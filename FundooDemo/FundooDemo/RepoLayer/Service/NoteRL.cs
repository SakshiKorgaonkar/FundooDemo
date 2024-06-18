using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.CustomException;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class NoteRL : INoteRL
    {
        private readonly ProjectContext projectContext;

        public NoteRL(ProjectContext projectContext)
        {
            this.projectContext = projectContext;
        }

        public NoteEntity AddNote(NoteML note)
        {
            NoteEntity note1 = new NoteEntity();
            note1.Title= note.Title;
            note1.Description= note.Description;
            projectContext.Notes.Add(note1);
            projectContext.SaveChanges();
            return note1;
        }

        public List<NoteEntity> GetAllNotes()
        {
            if (projectContext.Notes == null)
            {
                throw new CustomException1("No notes added");
            }
            List<NoteEntity> notes = new List<NoteEntity>();    
            foreach (NoteEntity note in projectContext.Notes)
            {
                if(note.isTrashed==false && note.isArchived == false)
                {
                    notes.Add(note);
                }
            }
            return notes.ToList();
        }

        public NoteEntity GetNoteById(int id)
        {
            var note = projectContext.Notes.FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            return note;
        }

        public NoteEntity RemoveNote(int id)
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

        public NoteEntity UpdateNote(int id,NoteML updatedNote)
        {
            var note = projectContext.Notes.FirstOrDefault(x => x.Id == id);
            if (note == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            note.Title = updatedNote.Title;
            note.Description = updatedNote.Description;
            projectContext.Notes.Update(note);
            projectContext.SaveChanges();
            return note;
        }
        public NoteEntity Archive(int id)
        {
            var noteToArchive=projectContext.Notes.FirstOrDefault(x=>x.Id== id);
            if(noteToArchive == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            noteToArchive.isArchived = !noteToArchive.isArchived;
            if(noteToArchive.isTrashed) 
            {
                throw new CustomException1("Note in trash cannot be sent in archived");
            }
            projectContext.Notes.Update(noteToArchive);
            projectContext.SaveChanges();
            return noteToArchive;
        }
        public NoteEntity Trash(int id)
        {
            var noteToTrash= projectContext.Notes.FirstOrDefault(x => x.Id == id);
            if (noteToTrash == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            noteToTrash.isTrashed = !noteToTrash.isTrashed;
            if (noteToTrash.isArchived)
            {
                noteToTrash.isArchived = !noteToTrash.isArchived;
            }
            projectContext.Notes.Update(noteToTrash);
            projectContext.SaveChanges();
            return noteToTrash;
        }
    }
}
