namespace Tienda_Online.Shared.DTOs
{
    public class CarritoConProductoDTO
    {
        public string FotoProducto { get; set; } = null!;
        public string NombreProducto { get; set; } = null!;
        public double PrecioTotal { get; set; } = 0!;
        public int CantidadProductos { get; set; } = 0!;

    }
}
