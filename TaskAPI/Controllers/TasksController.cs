using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Context;
using TaskModel = TaskAPI.Models.Task;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly TaskDbContext contextDb;

    public TasksController(TaskDbContext context)
    {
        contextDb = context;
    }

    // Obtener todas las tareas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskModel>>> GetTask()
    {
        return await contextDb.Tasks
            .Include(t => t.Items)
            .ToListAsync();
    }

    // Obtener una tarea por id
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskModel>> GetTask(int id)
    {
        if (id <= 0)
            return BadRequest("El id debe ser mayor que 0.");

        var task = await contextDb.Tasks
            .Include(t => t.Items)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
            return NotFound($"No existe una tarea con id {id}.");

        return Ok(task);
    }

    // Actualizar una tarea
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTask(int id, [FromBody] TaskModel task)
    {
        if (task == null)
            return BadRequest("Debe enviar los datos de la tarea.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id <= 0)
            return BadRequest("El id debe ser mayor que 0.");

        if (task.Id != id)
            return BadRequest("El id de la URL no coincide con el id del body.");

        var existingTask = await contextDb.Tasks.FindAsync(id);

        if (existingTask == null)
            return NotFound($"No existe una tarea con id {id}.");

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.IsCompleted = task.IsCompleted;

        await contextDb.SaveChangesAsync();

        return NoContent();
    }

    // Crear una tarea
    [HttpPost]
    public async Task<ActionResult<TaskModel>> PostTask([FromBody] TaskModel task)
    {
        if (task == null)
            return BadRequest("Debe enviar los datos de la tarea.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        task.CreatedAt = DateTime.UtcNow;

        contextDb.Tasks.Add(task);
        await contextDb.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTask),
            new { id = task.Id },
            task);
    }

    // Eliminar una tarea
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        if (id <= 0)
            return BadRequest("El id debe ser mayor que 0.");

        var task = await contextDb.Tasks.FindAsync(id);

        if (task == null)
            return NotFound($"No existe una tarea con id {id}.");

        contextDb.Tasks.Remove(task);
        await contextDb.SaveChangesAsync();

        return NoContent();
    }

    // Agregar item a una tarea
    [HttpPost("{taskId}/items")]
    public async Task<ActionResult<TaskAPI.Models.Item>> PostItem(
        int taskId,
        [FromBody] TaskAPI.Models.Item item)
    {
        if (taskId <= 0)
            return BadRequest("El id de la tarea debe ser mayor que 0.");

        if (item == null)
            return BadRequest("Debe enviar los datos del item.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var task = await contextDb.Tasks.FindAsync(taskId);

        if (task == null)
            return NotFound($"No existe una tarea con id {taskId}.");

        item.TaskId = taskId;

        contextDb.Items.Add(item);
        await contextDb.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTask),
            new { id = taskId },
            item);
    }

    private bool TaskExists(int id)
    {
        return contextDb.Tasks.Any(t => t.Id == id);
    }
}