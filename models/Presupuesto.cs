public class Presupuesto
{
    private int idPresupuesto;
    private string nombreDestinatario;
    private DateTime fechaCreacion;
    private List<PresupuestoDetalle> detalle;

    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }

    // Metodos
    public int montoPresupuesto()
    {
        return detalle.Sum(det => det.Producto.Precio * det.Cantidad);
    }

    public int montoPresupuestoConIva()
    {
        return (int)(detalle.Sum(det => det.Producto.Precio * det.Cantidad) * 1.21);
    }
    
    public int CantidadProductos()
    {
        return detalle.Sum(det => det.Cantidad);
    }
}