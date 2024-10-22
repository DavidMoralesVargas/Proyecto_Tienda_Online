using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Tienda_Online.Backend.Data;
using Tienda_Online.Shared.Entidades;
using Tienda_Online.Shared.Respuesta;

namespace Tienda_Online.Backend.Clases
{
    public class clsProducto
    {
        private readonly DataContext _context;

        public clsProducto(DataContext context)
        {
            _context = context;
        }

        public async Task<AccionRespuesta<Producto>> CrearProducto(Producto producto)
        {
            try
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return new AccionRespuesta<Producto>
                {
                    Exitoso = true,
                    Respuesta = producto
                };
            }catch (Exception)
            {
                return new AccionRespuesta<Producto>
                {
                    Exitoso = false,
                };
            }
        }

        public async Task<AccionRespuesta<IEnumerable<Producto>>> ObtenerListaProducto()
        {
            try
            {
                return new AccionRespuesta<IEnumerable<Producto>>
                {
                    Exitoso = true,
                    Respuesta = await _context.Productos.ToListAsync()
                };
            }
            catch (Exception)
            {
                return new AccionRespuesta<IEnumerable<Producto>>
                {
                    Exitoso = false
                };
            }
           
        }


    }
}
