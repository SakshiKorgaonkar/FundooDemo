using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity AddNote(NoteML note);
        public NoteEntity RemoveNote(int id);
        public List<NoteEntity> GetAllNotes();
        public NoteEntity GetNoteById(int id);
        public NoteEntity UpdateNote(int id,NoteML note);
        public NoteEntity Archive(int id);
        public NoteEntity Trash(int id);
        public List<NoteEntity> GetAllTrashNotes();
        public List<NoteEntity> GetAllArchiveNotes();
    }
}
