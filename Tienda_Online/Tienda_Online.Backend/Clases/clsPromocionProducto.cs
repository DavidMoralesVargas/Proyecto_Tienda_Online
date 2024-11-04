using Microsoft.EntityFrameworkCore;
using Tienda_Online.Backend.Data;
using Tienda_Online.Shared.Entidades;
using Tienda_Online.Shared.Respuesta;

namespace Tienda_Online.Backend.Clases
{
    public class clsPromocionProducto
    {
        private readonly DataContext _context;

        public clsPromocionProducto(DataContext context)
        {
            _context = context;
        }

        public async Task<AccionRespuesta<PromocionProducto>> CrearPromocion(PromocionProducto promocion)
        {
            try
            {
                _context.Add(promocion);
                await _context.SaveChangesAsync();
                return new AccionRespuesta<PromocionProducto>
                {
                    Exitoso = true,
                    Respuesta = promocion
                };
            }
            catch (Exception)
            {
                return new AccionRespuesta<PromocionProducto>
                {
                    Exitoso = false,
                };
            }
        }

        public async Task<AccionRespuesta<IEnumerable<object>>> ObtenerListaPromocionProducto()
        {
            try
            {
                var iqueryable = await (from PP in _context.Set<PromocionProducto>()
                                        join P in _context.Set<Producto>()
                                        on PP.ProductoId equals P.Id
                                        orderby P.Nombre
                                        select new
                                        {
                                            CodigoProducto = P.Id,
                                            NombreProducto = P.Nombre,
                                            CodigoPromocion = PP.Id,
                                            ValorPromocion = PP.PrecioOferta,
                                            ValorAntiguo = PP.PrecioAntiguo
                                        }).ToListAsync();
                return new AccionRespuesta<IEnumerable<object>>
                {
                    Exitoso = true,
                    Respuesta = iqueryable
                };
            }
            catch (Exception)
            {
                return new AccionRespuesta<IEnumerable<object>>
                {
                    Exitoso = false
                };
            }

        }
    }
}
