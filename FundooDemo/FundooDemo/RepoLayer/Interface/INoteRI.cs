using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface INoteRI
    {
        public Note AddNote(NoteMI note);
        public Note RemoveNote(int id);
        public List<Note> GetAllNotes();
        public Note GetNoteById(int id);
        public Note UpdateNote(int id,NoteMI note);
    }
}
