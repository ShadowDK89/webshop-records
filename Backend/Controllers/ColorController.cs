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
public class ColorController : ControllerBase
{
    private readonly WebshopContext _context;
    private readonly IMapper _mapper;
    public ColorController(WebshopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult> GetColors()
    {
        List<Color> colorList = await _context.Colors
            .ToListAsync();
        if (colorList == null)
        {
            return NotFound();
        }
        List<ColorDTO> colorListDTO = _mapper.Map<List<ColorDTO>>(colorList);
        return Ok(colorListDTO);
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetColorById(int id)
    {
        Color c = await _context.Colors.FindAsync(id);
        if (c == null)
        {
            return NotFound();
        }
        ColorDTO colorDTO = _mapper.Map<ColorDTO>(c);
        return Ok(colorDTO);

    }
    [HttpPost]
    public async Task<ActionResult> AddNewColor(ColorDTO newColorDTO)
    {
        Color newColor = _mapper.Map<Color>(newColorDTO);

        _context.Add(newColor);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {

            throw;
        }

        return CreatedAtAction("AddNewColor", newColor);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateColor(ColorDTO updateColorDTO, int id)
    {
        if (id != updateColorDTO.Id)
        {
            return BadRequest();
        }
        Color updateColor = _mapper.Map<Color>(updateColorDTO);
        _context.Entry(updateColor).State = EntityState.Modified;
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
    public async Task<ActionResult> deleteColor(int id)
    {
        Color deleteColor = await _context.Colors.FindAsync(id);
        if (deleteColor == null)
        {
            return NotFound();
        }
        try
        {
            _context.Remove(deleteColor);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }
}