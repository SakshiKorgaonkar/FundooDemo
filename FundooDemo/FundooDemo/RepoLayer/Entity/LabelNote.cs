using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class LabelNote
    {
        public int NoteId {  get; set; }
        public int LabelId {  get; set; }
        public Note Note { get; set; }
        public Label Label { get; set; }
    }
}
