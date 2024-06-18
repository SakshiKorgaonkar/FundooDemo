using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepoLayer.CustomException;

namespace FundooDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBL collaboratorBL;
        public CollaboratorController(ICollaboratorBL collaboratorBL)
        {
            this.collaboratorBL = collaboratorBL;
        }
        [HttpPost]
        public IActionResult AddCollaborator(int noteId,string email)
        {
            try
            {
                var result = collaboratorBL.AddCollaborator(noteId, email);
                return Ok(result);
            }
            catch (CustomException1 ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(500, result);
            }
        }
        [HttpGet]
        public IActionResult GetCollaboratorByNoteId(int noteId)
        {
            try
            {
                var result = collaboratorBL.GetCollaboratorByNoteId(noteId);
                return Ok(result);
            }
            catch (CustomException1 ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(500, result);
            }
        }
        [HttpDelete]
        public IActionResult RemoveCollaborator(int noteId, string email)
        {
            try
            {
                var result = collaboratorBL.RemoveCollaborator(noteId,email);
                return Ok(result);
            }
            catch (CustomException1 ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new DTO
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return StatusCode(500, result);
            }
        }
    }
}
