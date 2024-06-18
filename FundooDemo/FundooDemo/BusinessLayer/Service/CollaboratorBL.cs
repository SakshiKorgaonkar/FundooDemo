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
    public class CollaboratorBL : ICollaboratorBL
    {
        private readonly ICollaboratorRL collaboratorRL;
        public CollaboratorBL(ICollaboratorRL _collaboratorRL)
        {
            this.collaboratorRL = _collaboratorRL;
        }
        public string AddCollaborator(int noteId, string email)
        {
            try
            {
                return collaboratorRL.AddCollaborator(noteId, email);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<CollaboratorEntity> GetCollaboratorByNoteId(int noteId)
        {
            try
            {
                return collaboratorRL.GetCollaboratorByNoteId(noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string RemoveCollaborator(int noteId, string email)
        {
            try
            {
                return collaboratorRL.RemoveCollaborator(noteId, email);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
