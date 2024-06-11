using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBI
    {
        public Note AddNote(NoteMI note);
        public Note RemoveNote(int id);
        public List<Note> GetAllNotes();
        public Note GetNoteById(int id);
        public Note UpdateNote(int id,NoteMI note);
    }
}
