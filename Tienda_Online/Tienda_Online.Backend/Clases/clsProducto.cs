using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Tienda_Online.Backend.Data;
using Tienda_Online.Backend.Helpers;
using Tienda_Online.Shared.DTOs;
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

        //public async Task<AccionRespuesta<IEnumerable<Producto>>> ObtenerListaProducto()
        //{
        //    try
        //    {
        //        return new AccionRespuesta<IEnumerable<Producto>>
        //        {
        //            Exitoso = true,
        //            Respuesta = await _context.Productos.ToListAsync()
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        return new AccionRespuesta<IEnumerable<Producto>>
        //        {
        //            Exitoso = false
        //        };
        //    }
           
        //}

        public async Task<AccionRespuesta<IEnumerable<Producto>>> ObtenerListaProducto(PaginacionDTO paginacion)
        {
            var queryable = _context.Productos.AsQueryable();
            return new AccionRespuesta<IEnumerable<Producto>>
            {
                Exitoso = true,
                Respuesta = await queryable.Paginate(paginacion).ToListAsync()
            };
        }

        public async Task<AccionRespuesta<int>> ObtenerTotalPaginas(PaginacionDTO paginacion)
        {
            var queryable = _context.Productos.AsQueryable();
            double contador = await queryable.CountAsync();
            int totalPaginas = (int)Math.Ceiling(contador / paginacion.NumeroRegistros);
            return new AccionRespuesta<int>
            {
                Exitoso = true,
                Respuesta = totalPaginas
            };
        }

        public async Task<AccionRespuesta<Producto>> BuscarProducto(int id)
        {
            var _producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
            if(_producto == null)
            {
                return new AccionRespuesta<Producto>
                {
                    Exitoso = false
                };
            }
            return new AccionRespuesta<Producto>
            {
                Exitoso = true,
                Respuesta = _producto
            };
        }

        public async Task<AccionRespuesta<Producto>> ActualizarProducto(Producto producto)
        {
            _context.Update(producto);
            await _context.SaveChangesAsync();
            return new AccionRespuesta<Producto>
            {
                Exitoso = true,
                Respuesta = producto
            };
        }

        public async Task<AccionRespuesta<object>> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(o => o.Id == id);
            if(producto == null)
            {
                return new AccionRespuesta<object>
                {
                    Exitoso = false
                };
            }
            _context.Remove(producto);
            await _context.SaveChangesAsync();
            return new AccionRespuesta<object>
            {
                Exitoso = true
            };
        }
    }
}
