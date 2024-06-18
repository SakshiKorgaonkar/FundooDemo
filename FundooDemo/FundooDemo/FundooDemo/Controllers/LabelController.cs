using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepoLayer.CustomException;

namespace FundooDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBl;

        public LabelController(ILabelBL labelBl)
        {
            this.labelBl = labelBl;
        }
        [HttpPost]
        public IActionResult AddLabel(LabelML labelMl)
        {
            try
            {
                var result=labelBl.AddLabel(labelMl);
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
        public IActionResult GetLabels()
        {
            try
            {
                var result=labelBl.GetLabels();
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
        [HttpGet("byId")]
        public IActionResult GetLabel(int id)
        {
            try
            {
                var result = labelBl.GetLabel(id);
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
        [HttpPut]
        public IActionResult UpdateLabel(int id,LabelML label)
        {
            try
            {
                var result = labelBl.UpdateLabel(id, label);
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
        public IActionResult RemoveLabel(int id)
        {
            try
            {
                var result = labelBl.RemoveLabel(id);
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
