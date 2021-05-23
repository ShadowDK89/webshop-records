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
public class GenresController : ControllerBase
{
    private readonly WebshopContext _context;
    private readonly IMapper _mapper;
    public GenresController(WebshopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult> GetGenres()
    {
        List<Genre> genreList = await _context.Genres
        .ToListAsync();

        if (genreList == null)
        {
            return NotFound();
        }

        List<GenreDTO> genreListDTO = _mapper.Map<List<GenreDTO>>(genreList);
        return Ok(genreListDTO);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetGenreById(int id)
    {
        Genre g = await _context.Genres
        .FindAsync(id);

        if (g == null)
        {
            return NotFound();
        }
        GenreDTO genreDTO = _mapper.Map<GenreDTO>(g);
        return Ok(genreDTO);
    }

    [HttpPost]
    public async Task<ActionResult> AddGenre(GenreDTO newGenreDTO)
    {
        Genre newGenre = _mapper.Map<Genre>(newGenreDTO);

        _context.Genres.Add(newGenre);
        await _context.SaveChangesAsync();

        return Ok(newGenre);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateProduct(GenreDTO updateGenreDTO, int id)
    {
        if (id != updateGenreDTO.Id)
        {
            return BadRequest();
        }
        Genre updateGenre = _mapper.Map<Genre>(updateGenreDTO);
        _context.Entry(updateGenre).State = EntityState.Modified;

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
    public async Task<ActionResult> DeleteGenre(int id)
    {
        Genre deleteGenre = await _context.Genres.FindAsync(id);

        if (deleteGenre == null)
        {
            return NotFound();
        }
        _context.Genres.Remove(deleteGenre);
        await _context.SaveChangesAsync();

        return Ok();
    }
}