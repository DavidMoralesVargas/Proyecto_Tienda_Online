using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Tienda_Online.Backend.Clases;
using Tienda_Online.Shared.Entidades;

namespace Tienda_Online.Backend.Controllers
{
    [ApiController]
    [Route("/api/promociones")]
    public class PromocionProductoController : ControllerBase
    {
        private readonly clsPromocionProducto _promocion;
        public PromocionProductoController(clsPromocionProducto promocion)
        {
            _promocion = promocion;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerListaPromocionProducto()
        {
            var promociones = await _promocion.ObtenerListaPromocionProducto();
            if (promociones.Exitoso)
            {
                return Ok(promociones.Respuesta);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPromocion(PromocionProducto promocion)
        {
            var promociones = await _promocion.CrearPromocion(promocion);
            if(promociones.Exitoso)
            {
                return Ok(promociones.Respuesta);
            }
            return BadRequest();
        }
    }
}
