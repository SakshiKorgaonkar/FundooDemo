using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class LabelNoteEntity
    {
        public int NoteId {  get; set; }
        public int LabelId {  get; set; }
        public NoteEntity Note { get; set; }
        public LabelEntity Label { get; set; }
    }
}