using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Tienda_Online.Backend.Data;
using Tienda_Online.Backend.Helpers;
using Tienda_Online.Shared.DTOs;
using Tienda_Online.Shared.Entidades;
using Tienda_Online.Shared.Respuesta;

namespace Tienda_Online.Backend.Clases
{
    public class clsCarritoCompra
    {
        private readonly DataContext _context;
        private readonly clsProducto _clsProducto;

        public clsCarritoCompra(DataContext context, clsProducto clsProducto)
        {
            _context = context;
            _clsProducto = clsProducto;
        }

        public async Task<AccionRespuesta<CarritoDeCompra>> CrearCarritoCompra(CarritoDeCompra carrito)
        {
            try
            {
                var producto = await _clsProducto.BuscarProducto(carrito.ProductoId);
                if (producto == null)
                {
                    return new AccionRespuesta<CarritoDeCompra>
                    {
                        Exitoso = false
                    };
                }
                carrito.ValorCantidadProducto = producto.Respuesta!.Precio * carrito.CantidadProducto;
                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return new AccionRespuesta<CarritoDeCompra>
                {
                    Exitoso = true,
                    Respuesta = carrito
                };
            }
            catch (Exception)
            {
                return new AccionRespuesta<CarritoDeCompra>
                {
                    Exitoso = false,
                };
            }
        }

        public async Task<AccionRespuesta<IEnumerable<CarritoConProductoDTO>>> ObtenerListaCarrito()
        {
            var query = from CC in _context.Set<CarritoDeCompra>()
                                   join P in _context.Set<Producto>()
                                   on CC.ProductoId equals P.Id
                                   select new CarritoConProductoDTO
                                   {
                                       FotoProducto = P.Foto,
                                       NombreProducto = P.Nombre,
                                       PrecioTotal = CC.ValorCantidadProducto,
                                       CantidadProductos = CC.CantidadProducto
                                   };

            var result = await query.ToListAsync();
            return new AccionRespuesta<IEnumerable<CarritoConProductoDTO>>
            {
                Exitoso = true,
                Respuesta = result


            };
        }
    }
}
