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
        private readonly ILabelNoteRI labelNoteRI;
        public LabelNoteBL(ILabelNoteRI labelNoteRI)
        {
            this.labelNoteRI = labelNoteRI;
        }

        public Label AddLabelToNote(int labelId, int noteId)
        {
            try
            {
                return labelNoteRI.AddLabelToNote(labelId, noteId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<Label> GetAllLabelsFromNote(int noteId)
        {
            try
            {
                return labelNoteRI.GetAllLabelsFromNote(noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Note> GetAllNotesFromLabel(int labelId)
        {
            try
            {
                return labelNoteRI.GetAllNotesFromLabel(labelId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Label RemoveLabelFromNote(int labelId, int noteId)
        {
            try
            {
                return labelNoteRI.RemoveLabelFromNote(labelId, noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
