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
public class ProductController : ControllerBase
{
    private readonly WebshopContext _context;
    private readonly IMapper _mapper;
    public ProductController(WebshopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetProducts()
    {
        try
        {
            List<Product> productList = await _context.Products
            .Include(p => p.Color)
            .Include(p => p.Genre)
            .Include(p => p.TrackList)
            .Include(p => p.Format)
            .ToListAsync();
            List<ProductDTO> productDTOs = _mapper.Map<List<ProductDTO>>(productList);

            return Ok(productDTOs);
        }
        catch (System.ArgumentNullException)
        {
            return NotFound();
        }
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetProductById(int id)
    {
        Product p = await _context.Products
        .Include(p => p.Color)
        .Include(p => p.Format)
        .Include(p => p.Genre)
        .Include(p => p.TrackList)
        .SingleAsync(p => p.Id == id);

        if (p == null)
        {
            return NotFound();
        }

        ProductDTO productDTO = _mapper.Map<ProductDTO>(p);
        return Ok(productDTO);
    }


    [HttpPost]
    public async Task<ActionResult> AddNewProduct(ProductDTO newProductDTO)
    {

        Product newProduct = _mapper.Map<Product>(newProductDTO);
        //If adding a new product, code below makes sure to
        //either add new value or 'attach' already existing value
        //for Color, Format and Genre
        foreach (Color c in newProduct.Color)
        {
            if (c.Id == 0)
                _context.Set<Color>().Add(c);
            else
                _context.Set<Color>().Attach(c);
        }

        foreach (Format f in newProduct.Format)
        {
            if (f.Id == 0)
                _context.Set<Format>().Add(f);
            else
                _context.Set<Format>().Attach(f);
        }

        foreach (Genre g in newProduct.Genre)
        {
            if (g.Id == 0)
                _context.Set<Genre>().Add(g);
            else
                _context.Set<Genre>().Attach(g);
        }

        _context.Set<Product>().Attach(newProduct);

        _context.Add(newProduct);
        await _context.SaveChangesAsync();

        return CreatedAtAction("AddNewProduct", newProduct);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateProduct(ProductDTO updateProductDTO, int id)
    {
        if (id != updateProductDTO.Id)
        {
            return BadRequest();
        }
        Product updateProduct = _mapper.Map<Product>(updateProductDTO);
        _context.Update(updateProduct);

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
        Product p = await _context.Products
        .SingleAsync(p => p.Id == id);
        if (p == null)
        {
            NotFound();
        }

        try
        {
            _context.Remove(p);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
        return NoContent();
    }
}
