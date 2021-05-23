using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly WebshopContext _context;
    private readonly IMapper _mapper;
    public OrderController(WebshopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetOrders()
    {
        List<Order> orderList = await _context.Orders
        .Include(o => o.OrderProducts)
        .ThenInclude(p => p.Product)
        .Include(o => o.OrderProducts)
        .ThenInclude(o => o.Color)
        .Include(o => o.OrderProducts)
        .ThenInclude(f => f.Format)
        .ToListAsync();

        List<OrderDTO> orderListDTO = _mapper.Map<List<OrderDTO>>(orderList);

        if (orderListDTO == null)
        {
            return NotFound();
        }

        return Ok(orderListDTO);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetOrderById(int id)
    {
        Order order = await _context.Orders
        .Include(o => o.OrderProducts)
        .ThenInclude(p => p.Product)
        .Include(o => o.OrderProducts)
        .ThenInclude(o => o.Color)
        .Include(o => o.OrderProducts)
        .ThenInclude(f => f.Format)
        .SingleAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }
        OrderDTO orderDTO = _mapper.Map<OrderDTO>(order);

        return Ok(orderDTO);
    }

    [HttpPost]
    public async Task<ActionResult> AddNewOrder(OrderDTO newOrderDTO)
    {

        Order newOrder = _mapper.Map<Order>(newOrderDTO);
        newOrder.DateCreated = DateTime.Now;

        _context.Add(newOrder);
        await _context.SaveChangesAsync();

        return CreatedAtAction("AddNewOrder", _mapper.Map<OrderDTO>(newOrder));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateOrder(OrderDTO orderDTO, int id)
    {
        if (id != orderDTO.Id)
        {
            return BadRequest();
        }
        Order orderToUpdate = _mapper.Map<Order>(orderDTO);
        _context.Update(orderToUpdate);

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
    public async Task<ActionResult> DeleteOrder(int id)
    {
        Order o = await _context.Orders
        .SingleAsync(o => o.Id == id);
        if (o == null)
        {
            NotFound();
        }
        _context.Remove(o);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}