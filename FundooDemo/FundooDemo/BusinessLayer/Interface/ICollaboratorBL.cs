using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorBL
    {
        public List<CollaboratorEntity> GetCollaboratorByNoteId(int noteId);
        public string RemoveCollaborator(int noteId, string email);
        public string AddCollaborator(int noteId, string email);
    }
}
