using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.CustomException;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Utility;

namespace FundooDemo.Controllers
{
    [Route("api/notes")]
    [ApiController]
    [EnableCors("corspolicy")]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBl;
        private readonly Caching<NoteEntity> _caching;
        private readonly RabbitMQProducer _rabitMQProducer;
        private readonly ILogger<NoteController> _logger;
        private readonly ProjectContext projectContext1;
        public NoteController(INoteBL noteBl,ProjectContext projectContext,IDistributedCache cache,RabbitMQProducer rabbitMQProducer,ILogger<NoteController> logger,ProjectContext projectContext1)
        {
            this._rabitMQProducer = rabbitMQProducer;
            this.noteBl = noteBl;
            this._caching = new Caching<NoteEntity>(projectContext, cache);
            this._logger = logger;
            _logger.LogDebug("Nlog is integrated to Note Controller");
            this.projectContext1 = projectContext1;
        }
        [HttpPost]
        public IActionResult AddNote(NoteML note)
        {
            try
            {
                var result = noteBl.AddNote(note);
                _rabitMQProducer.SendProductMessage(result);
                _caching.Update("AllNotes");
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
                _logger.LogError(ex.Message);
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
        public IActionResult RemoveNote(int id)
        {
            try
            {
                var result = noteBl.RemoveNote(id);
                _caching.Update("AllNotes");
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
        [HttpGet("all")]
        public IActionResult GetAllNotes()
        {
            try
            {
                var notesWithLabels = _caching.GetAll("allNotesCacheKey", () =>
                {
                   return projectContext1.Notes.Include(n => n.LabelNotes)
                        .ThenInclude(nl => nl.Label)
                        .Select(n => new
                        {
                            n.Id,
                            n.Title,
                            n.Description,
                            n.isTrashed,
                            n.isArchived,
                            Labels = n.LabelNotes.Select(nl => new
                            {
                                LabelId = nl.Label.Id,
                                LabelName = nl.Label.Name,
                            }).ToList()
                        })
                        .ToList();
                });

                // Check if the results are empty and throw an exception if necessary
                if (!notesWithLabels.Any())
                {
                    throw new CustomException1("No labels and notes found");
                }

                // Return the results
                return Ok(notesWithLabels);
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
        [HttpGet("byId")]
        public IActionResult GetNoteById(int id)
        {
            try
            {
                var note = _caching.GetById($"Note_{id}", () => noteBl.GetNoteById(id));
                return Ok(note);
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
        [HttpPut]
        public IActionResult UpdateNote(int id,NoteML note)
        {
            try
            {
                var result = noteBl.UpdateNote(id, note);
                _caching.Update("AllNotes");
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
        [HttpPut("archive")]
        public IActionResult Archive(int id)
        {
            try
            {
                var result = noteBl.Archive(id);
                _caching.Update("AllNotes");
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
        [HttpPut("trash")]
        public IActionResult Trash(int id)
        {
            try
            {
                var result = noteBl.Trash(id);
                _caching.Update("AllNotes");
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
        [HttpGet("allTrash")]
        public IActionResult GetAllTrashNotes()
        {
            try
            {
                var result = noteBl.GetAllTrashNotes();
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
        [HttpGet("allArchive")]
        public IActionResult GetAllArchiveNotes()
        {
            try
            {
                var result = noteBl.GetAllArchiveNotes();
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
