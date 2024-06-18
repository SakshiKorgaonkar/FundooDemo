using BusinessLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class LabelNoteBL : ILabelNoteBL
    {
        private readonly ILabelNoteRL labelNoteRl;
        public LabelNoteBL(ILabelNoteRL labelNoteRl)
        {
            this.labelNoteRl = labelNoteRl;
        }

        public LabelEntity AddLabelToNote(int labelId, int noteId)
        {
            try
            {
                return labelNoteRl.AddLabelToNote(labelId, noteId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<LabelEntity> GetAllLabelsFromNote(int noteId)
        {
            try
            {
                return labelNoteRl.GetAllLabelsFromNote(noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<NoteEntity> GetAllNotesFromLabel(int labelId)
        {
            try
            {
                return labelNoteRl.GetAllNotesFromLabel(labelId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public LabelEntity RemoveLabelFromNote(int labelId, int noteId)
        {
            try
            {
                return labelNoteRl.RemoveLabelFromNote(labelId, noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
