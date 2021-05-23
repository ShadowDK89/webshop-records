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
public class TrackListController : ControllerBase
{
    private readonly WebshopContext _context;
    private readonly IMapper _mapper;
    public TrackListController(WebshopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult> GetTrackList()
    {
        List<TrackList> trackList = await _context.TrackLists
            .ToListAsync();
        if (trackList == null)
        {
            return NotFound();
        }
        List<TrackListDTO> trackListDTO = _mapper.Map<List<TrackListDTO>>(trackList);
        return Ok(trackListDTO);
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetTrackById(int id)
    {
        TrackList t = await _context.TrackLists
        .FindAsync(id);
        if (t == null)
        {
            return NotFound();
        }
        TrackListDTO trackDTO = _mapper.Map<TrackListDTO>(t);
        return Ok(trackDTO);
    }
    [HttpPost]
    public async Task<ActionResult> AddNewTrackList(List<TrackListDTO> newTrackListDTO)
    {
        List<TrackList> newTrackList = _mapper.Map<List<TrackList>>(newTrackListDTO);
        _context.AddRange(newTrackList);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest();
            throw;
        }
        return CreatedAtAction("AddNewTrackList", newTrackList);
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateProduct(TrackListDTO updateTrackDTO, int id)
    {
        if (id != updateTrackDTO.Id)
        {
            return BadRequest();
        }
        TrackList updateTrack = _mapper.Map<TrackList>(updateTrackDTO);
        _context.Update(updateTrack);

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
    public async Task<ActionResult> DeleteProduct(int id)
    {
        TrackList t = await _context.TrackLists
        .SingleAsync(t => t.Id == id);
        if (t == null)
        {
            NotFound();
        }

        try
        {
            _context.Remove(t);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
        return NoContent();
    }
}