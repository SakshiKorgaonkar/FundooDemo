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
    public class NoteBI : INoteBI
    {
        private readonly INoteRI noteRI;

        public NoteBI(INoteRI _noteRI)
        {
            this.noteRI = _noteRI;
        }
        public Note AddNote(NoteMI note)
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

        public Note Archive(int id)
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

        public List<Note> GetAllNotes()
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

        public Note GetNoteById(int id)
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

        public Note RemoveNote(int id)
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

        public Note Trash(int id)
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

        public Note UpdateNote(int id,NoteMI note)
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
