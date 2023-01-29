using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;

namespace apiSupinfo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly DbFactoryContext _context;

    public CartController(DbFactoryContext context)
    {
        _context = context;
    }
    

    // GET: api/Carts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Cart>> GetCart(int id)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(cp => cp.Product)
            .FirstOrDefaultAsync(c => c.Id == id);

        return cart == null ? NotFound() : cart;
    }

    // PUT: api/Carts/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCart(int id, Cart cart)
    {
        if (id != cart.Id)
        {
            return BadRequest();
        }

        _context.Entry(cart).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CartExists(id)) return NotFound(); 
        }

        return NoContent();
    }

    // POST: api/Carts
    [HttpPost]
    public async Task<ActionResult<Cart>> PostCart(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCart", new {id = cart.Id}, cart);
    }

    // DELETE: api/Carts/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Cart>> DeleteCart(int id)
    {
        var cart = await _context.Carts.FindAsync(id);
        if (cart == null)
        {
            return NotFound();
        }

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();

        return cart;
    }

    private bool CartExists(int id)
    {
        return _context.Carts.Any(e => e.Id == id);
    }
}