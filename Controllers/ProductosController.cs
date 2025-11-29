using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class ProductosController : ControllerBase
{
    private readonly ProductoRepository productoRepository;
    public ProductosController()
    {
        productoRepository = new ProductoRepository();
    }

    [HttpPost("Producto")]
    public ActionResult<string> CreateProducto(Producto nuevoProducto)
    {
        if (string.IsNullOrEmpty(nuevoProducto.Descripcion))
            return BadRequest("La descripcion es obligatoria");

        if (nuevoProducto.Precio < 0)
            return BadRequest("El precio no puede ser negativo");

        productoRepository.Create(nuevoProducto);
        return Ok("Producto dado de alta exitosamente");
    }

    [HttpPut("Producto/{id}")]
    public IActionResult UpdateProducto(int id, [FromBody] Producto nuevoProducto)
    {
        var producto = productoRepository.GetById(id);

        if (producto == null)
            return NotFound("No se encontro el producto");

        // Validaciones
        if (string.IsNullOrEmpty(nuevoProducto.Descripcion))
            return BadRequest("La descripcion es obligatoria");

        if (nuevoProducto.Precio < 0)
            return BadRequest("El precio no puede ser negativo");

        productoRepository.Update(id, nuevoProducto);
        return Ok("Producto modificado con exito");
    }

    [HttpGet("Producto")]
    public ActionResult<List<Producto>> GetProductos()
    {
        List<Producto> listProductos;
        listProductos = productoRepository.GetAll();
        return Ok(listProductos);
    }

    [HttpGet("Producto/{id}")]
    public ActionResult<Producto> GetProductoPorId(int id)
    {
        var producto = productoRepository.GetById(id);

        if (producto == null)
            return NotFound("Producto no encontrado");

        return Ok(producto);
    }

    [HttpDelete("Producto/{id}")]
    public ActionResult DeleteProducto(int id)
    {
        var producto = productoRepository.GetById(id);

        if (producto == null)
            return NotFound("Producto no encontrado");

        productoRepository.DeleteById(id);
        return Ok("Producto eliminado");
    }
}