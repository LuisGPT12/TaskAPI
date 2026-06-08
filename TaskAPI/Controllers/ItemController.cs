using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Context;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly TaskDbContext contextDb;

    public ItemController(TaskDbContext context)
    {
        contextDb = context;
    }

    // Marcar un item como completado
    [HttpPut("{id}")]
    public async Task<IActionResult> PutItem(int id)
    {
        if (id <= 0)
            return BadRequest("El id debe ser mayor que 0.");

        var item = await contextDb.Items.FindAsync(id);

        if (item == null)
            return NotFound($"No existe un item con id {id}.");

        if (item.IsDone)
            return BadRequest("El item ya se encuentra completado.");

        item.IsDone = true;

        await contextDb.SaveChangesAsync();

        return NoContent();
    }

    // Eliminar un item
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        if (id <= 0)
            return BadRequest("El id debe ser mayor que 0.");

        var item = await contextDb.Items.FindAsync(id);

        if (item == null)
            return NotFound($"No existe un item con id {id}.");

        contextDb.Items.Remove(item);

        await contextDb.SaveChangesAsync();

        return NoContent();
    }
}