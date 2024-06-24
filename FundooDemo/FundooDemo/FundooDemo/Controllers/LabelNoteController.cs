using BusinessLayer.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepoLayer.CustomException;
using RepoLayer.Entity;

namespace FundooDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class LabelNoteController : ControllerBase
    {
        private readonly ILabelNoteBL labelNoteBL;
        public LabelNoteController(ILabelNoteBL labelNoteBL)
        {
            this.labelNoteBL = labelNoteBL;
        }
        [HttpPost]
        public IActionResult AddLabelToNote(int labelId, int noteId)
        {
            try
            {
                var result = labelNoteBL.AddLabelToNote(labelId, noteId);
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
        [HttpGet("getLabels")]
        public IActionResult GetAllLabelsFromNote(int noteId)
        {
            try
            {
                var result = labelNoteBL.GetAllLabelsFromNote(noteId);
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
        [HttpGet("getNotes")]
        public IActionResult GetAllNotesFromLabel(int labelId)
        {
            try
            {
                var result = labelNoteBL.GetAllNotesFromLabel(labelId);
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
        public IActionResult RemoveLabelFromNote(int labelId, int noteId)
        {
            try
            {
                var result = labelNoteBL.RemoveLabelFromNote(labelId,noteId);
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
