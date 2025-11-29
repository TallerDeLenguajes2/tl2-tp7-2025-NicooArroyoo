using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class PresupuestosController : ControllerBase
{
    private readonly PresupuestoRepository presupuestoRepository;
    public PresupuestosController()
    {
        presupuestoRepository = new PresupuestoRepository();
    }

    [HttpPost("Presupuesto")]
    public ActionResult<string> CreatePresupuesto(Presupuesto nuevoPresupuesto)
    {
        if (string.IsNullOrEmpty(nuevoPresupuesto.NombreDestinatario))
            return BadRequest("La descripcion es obligatoria");

        if (nuevoPresupuesto.FechaCreacion == default)
            return BadRequest("La fecha es obligatoria");

        presupuestoRepository.Create(nuevoPresupuesto);
        return Ok("Presupuesto dado de alta exitosamente");
    }

    [HttpPost("Presupuesto/{id}/ProductoDetalle")]
    public ActionResult<string> AgregarDetalle(int id, Producto producto, int cantidad)
    {
        var presupuesto = presupuestoRepository.GetById(id);

        if (presupuesto == null)
            return NotFound("No se encontro el presupuesto");

        if (string.IsNullOrEmpty(producto.Descripcion))
            return BadRequest("La descripcion del producto es obligatoria");

        if (producto.Precio < 0)
            return BadRequest("El precio no puede ser negativo");

        if (cantidad <= 0)
            return BadRequest("La cantidad debe ser positiva");

        PresupuestoDetalle detalle = new PresupuestoDetalle
        {
            Producto = producto,
            Cantidad = cantidad
        };

        presupuestoRepository.CreateDetalle(id, detalle);

        return Ok("Detalle agregado exitosamente");
    }

    [HttpGet("Presupuesto")]
    public ActionResult<List<Producto>> GetPresupuestos()
    {
        List<Presupuesto> listPresupuestos;
        listPresupuestos = presupuestoRepository.GetAll();
        return Ok(listPresupuestos);
    }

    [HttpGet("Presupuesto/{id}")]
    public ActionResult<Producto> GetPresupuestoPorId(int id)
    {
        var presupuesto = presupuestoRepository.GetById(id);

        if (presupuesto == null)
            return NotFound("Presupuesto no encontrado");

        return Ok(presupuesto);
    }

    [HttpDelete("Presupuesto/{id}")]
    public ActionResult DeletePresupuesto(int id)
    {
        var presupuesto = presupuestoRepository.GetById(id);

        if (presupuesto == null)
            return NotFound("Presupuesto no encontrado");

        presupuestoRepository.DeleteById(id);
        return Ok("Presupuesto eliminado");
    }
}