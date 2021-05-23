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
public class CustomerController : ControllerBase
{
    private readonly WebshopContext _context;
    private readonly IMapper _mapper;
    public CustomerController(WebshopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult> GetCustomers()
    {
        List<Customer> customerList = await _context.Customers
        .ToListAsync();

        if (customerList == null)
        {
            return NotFound();
        }

        List<CustomerDTO> customerListDTO = _mapper.Map<List<CustomerDTO>>(customerList);
        return Ok(customerListDTO);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetCustomerById(int id)
    {
        Customer c = await _context.Customers
        .FindAsync(id);

        if (c == null)
        {
            return NotFound();
        }
        CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(c);
        return Ok(customerDTO);
    }

    [HttpPost]
    public async Task<ActionResult> AddCustomer(CustomerDTO newCustomerDTO)
    {
        Customer newCustomer = _mapper.Map<Customer>(newCustomerDTO);

        _context.Customers.Add(newCustomer);
        await _context.SaveChangesAsync();

        return CreatedAtAction("AddCustomer", newCustomer);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateCustomer(CustomerDTO updateCustomerDTO, int id)
    {
        if (id != updateCustomerDTO.Id)
        {
            return BadRequest();
        }
        Customer updateCustomer = _mapper.Map<Customer>(updateCustomerDTO);
        _context.Entry(updateCustomer).State = EntityState.Modified;

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
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        Customer deleteCustomer = await _context.Customers
            .FindAsync(id);
        if (deleteCustomer == null)
        {
            return NotFound();
        }
        _context.Customers.Remove(deleteCustomer);
        await _context.SaveChangesAsync();
        return Ok();
    }
}