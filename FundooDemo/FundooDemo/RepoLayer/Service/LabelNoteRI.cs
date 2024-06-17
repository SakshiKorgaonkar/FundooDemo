using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.CustomException;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class LabelNoteRI : ILabelNoteRI
    {
        private readonly ProjectContext projectContext;
        public LabelNoteRI(ProjectContext _projectContext)
        {
            projectContext = _projectContext;
        }

        public Label AddLabelToNote(int labelId, int noteId)
        {
            var note=projectContext.Notes.FirstOrDefault(x=>x.Id==noteId);
            var label=projectContext.Labels.FirstOrDefault(x=>x.Id==labelId);
            if (note==null || label==null) 
            {
                throw new CustomException1("Note/Label doesn't exist");
            }
            var labelNote=new LabelNote { NoteId=noteId ,LabelId=labelId};
            projectContext.LabelNotes.Add(labelNote);
            projectContext.SaveChanges();
            return label;
        }

        public List<Label> GetAllLabelsFromNote(int noteId)
        {
            var note= projectContext.Notes.Include(n=>n.LabelNotes)
                                        .ThenInclude(ln=>ln.Label)
                                        .FirstOrDefault(n=>n.Id==noteId);
            if (note == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            return note.LabelNotes.Select(n=>n.Label).ToList();
        }

        public List<Note> GetAllNotesFromLabel(int labelId)
        {
            var label = projectContext.Labels.Include(l => l.LabelNotes)
                                         .ThenInclude(ln => ln.Note)
                                         .FirstOrDefault(l => l.Id == labelId);
            if (label == null)
            {
                throw new CustomException1("Label doesn't exist");
            }

            return label.LabelNotes.Select(ln => ln.Note).ToList();
        }

        public Label RemoveLabelFromNote(int labelId, int noteId)
        {
            var labelNote = projectContext.LabelNotes.FirstOrDefault(ln => ln.NoteId == noteId && ln.LabelId == labelId);
            if (labelNote == null)
            {
                throw new CustomException1("Label/Note association doesn't exist");
            }

            projectContext.LabelNotes.Remove(labelNote);
            projectContext.SaveChanges();

            return projectContext.Labels.FirstOrDefault(l => l.Id == labelId);
        }
    }
}
