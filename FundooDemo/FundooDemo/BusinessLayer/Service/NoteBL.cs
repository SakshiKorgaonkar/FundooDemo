using BusinessLayer.Interface;
using ModelLayer;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class NoteBL : INoteBL
    {
        private readonly INoteRL noteRI;

        public NoteBL(INoteRL _noteRI)
        {
            this.noteRI = _noteRI;
        }
        public NoteEntity AddNote(NoteML note)
        {
            try
            {
               return noteRI.AddNote(note);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NoteEntity Archive(int id)
        {
            try
            {
                return noteRI.Archive(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<NoteEntity> GetAllNotes()
        {
            try
            {
                return noteRI.GetAllNotes();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NoteEntity GetNoteById(int id)
        {
            try
            {
                return noteRI.GetNoteById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NoteEntity RemoveNote(int id)
        {
            try
            {
                return noteRI.RemoveNote(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NoteEntity Trash(int id)
        {
            try
            {
                return noteRI.Trash(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NoteEntity UpdateNote(int id,NoteML note)
        {
            try
            {
                return noteRI.UpdateNote(id,note);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
