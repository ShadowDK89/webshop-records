using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


[ApiController]
[Route("api/[controller]")]
public class FormatController : ControllerBase
{
    private readonly WebshopContext _context;
    private readonly IMapper _mapper;
    public FormatController(WebshopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult> GetFormats()
    {
        List<Format> formatList = await _context.Formats
             .ToListAsync();
        if (formatList == null)
        {
            return NotFound();
        }
        List<FormatDTO> formatListDTO = _mapper.Map<List<FormatDTO>>(formatList);
        return Ok(formatListDTO);
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateProduct(FormatDTO updateFormatDTO, int id)
    {
        if (id != updateFormatDTO.Id)
        {
            return BadRequest();
        }
        Format updateFormat = _mapper.Map<Format>(updateFormatDTO);
        _context.Entry(updateFormat).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteFormat(int id)
    {
        Format f = await _context.Formats
        .SingleAsync(f => f.Id == id);
        if (f == null)
        {
            NotFound();
        }

        try
        {
            _context.Remove(f);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
        return NoContent();
    }
}