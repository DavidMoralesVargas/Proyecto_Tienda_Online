using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata.Ecma335;
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
            catch (Exception ex)
            {
                return new AccionRespuesta<CarritoDeCompra>
                {
                    Mensaje = ex.Message,
                    Exitoso = false
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
                                       IdCarrito = CC.Id,
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

        public async Task<AccionRespuesta<CarritoDeCompra>> Actualizar(CarritoDeCompra carritoDeCompra)
        {
            try
            {
                var producto = await _clsProducto.BuscarProducto(carritoDeCompra.ProductoId);
                if (producto == null)
                {
                    return new AccionRespuesta<CarritoDeCompra>
                    {
                        Exitoso = false
                    };
                }
                carritoDeCompra.ValorCantidadProducto = producto.Respuesta!.Precio * carritoDeCompra.CantidadProducto;
                _context.Update(carritoDeCompra);
                await _context.SaveChangesAsync();
                return new AccionRespuesta<CarritoDeCompra>
                {
                    Exitoso = true,
                    Respuesta = carritoDeCompra
                };
            }
            catch(Exception ex)
            {
                return new AccionRespuesta<CarritoDeCompra>
                {
                    Mensaje = ex.Message,
                    Exitoso = false
                };
            }
        }

        public async Task<AccionRespuesta<CarritoDeCompra>> BuscarCarritoCompra(int id)
        {
            try
            {
                var carrito = await _context.CarritosDeCompra.FirstOrDefaultAsync(c => c.Id == id);
                if(carrito == null)
                {
                    return new AccionRespuesta<CarritoDeCompra>
                    {
                        Mensaje = "El carrito de compra no se encuentra en la base de datos",
                        Exitoso = false
                    };
                }
                return new AccionRespuesta<CarritoDeCompra>
                {
                    Exitoso = true,
                    Respuesta = carrito
                };
            }
            catch(Exception e)
            {
                return new AccionRespuesta<CarritoDeCompra>
                {
                    Mensaje = e.Message,
                    Exitoso = false
                };
            }
        }

        public async Task<AccionRespuesta<CarritoDeCompra>> EliminarProducto(int id)
        {
            try
            {
                var carrito = await _context.CarritosDeCompra.FirstOrDefaultAsync(c => c.Id == id);
                if(carrito == null)
                {
                    return new AccionRespuesta<CarritoDeCompra>
                    {
                        Mensaje = "Carrito no se encuentra en la base de datos",
                        Exitoso = false
                    };
                }
                _context.Remove(carrito);
                await _context.SaveChangesAsync();
                return new AccionRespuesta<CarritoDeCompra>
                {
                    Exitoso = true,
                    Respuesta = carrito
                };
            }
            catch(Exception e)
            {
                return new AccionRespuesta<CarritoDeCompra>
                {
                    Mensaje = e.Message,
                    Exitoso = false
                };
            }
        }

        public async Task<AccionRespuesta<bool>> EliminarRegistros()
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("DELETE FROM CarritosDeCompra");
                return new AccionRespuesta<bool>
                {
                    Respuesta = true,
                    Exitoso = true
                };
            }
            catch (Exception ex)
            {
                return new AccionRespuesta<bool>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }
    }
}
