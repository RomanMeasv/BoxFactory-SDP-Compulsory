using System.ComponentModel.DataAnnotations;
using Application.DTOs;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace BoxFactoryAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BoxApiController : ControllerBase
{
    private IBoxService _boxService;

    public BoxApiController(IBoxService boxService)
    {
        _boxService = boxService;
    }

    [HttpGet(Name = "GetAllBoxes")]
    public ActionResult<List<Box>> GetAllBoxes()
    {
        return _boxService.GetAllBoxes();
    }

    [HttpPost]
    [Route("")]
    public ActionResult<Box> CreateNewBox(PostBoxDTO dto)
    {
        try
        {
            var result = _boxService.CreateNewBox(dto);
            return Created("", result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Box> GetBoxById(int id)
    {
        try
        {
            return _boxService.GetBoxById(id);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound("No box with ID " + id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult<Box> UpdateBox([FromRoute] int id, [FromBody] Box box)
    {
        try
        {
            return Ok(_boxService.UpdateBox(id, box));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound("No box with ID " + id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult<Box> DeleteBox(int id)
    {
        try
        {
            return Ok(_boxService.DeleteBox(id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound("No box with ID " + id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet]
    [Route("RebuildDB")]
    public void RebuildDB()
    {
        _boxService.RebuildDb();
    }
}