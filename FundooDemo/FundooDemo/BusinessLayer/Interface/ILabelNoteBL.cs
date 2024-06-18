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
        public LabelEntity AddLabelToNote(int labelId, int noteId);
        public LabelEntity RemoveLabelFromNote(int labelId, int noteId);
        public List<LabelEntity> GetAllLabelsFromNote(int noteId);
        public List<NoteEntity> GetAllNotesFromLabel(int labelId);
    }
}
