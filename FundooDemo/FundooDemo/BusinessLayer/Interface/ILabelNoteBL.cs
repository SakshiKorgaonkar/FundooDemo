using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelNoteBL
    {
        public Label AddLabelToNote(int labelId, int noteId);
        public Label RemoveLabelFromNote(int labelId, int noteId);
        public List<Label> GetAllLabelsFromNote(int noteId);
        public List<Note> GetAllNotesFromLabel(int labelId);
    }
}
