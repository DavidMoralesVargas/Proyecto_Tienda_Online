using Microsoft.AspNetCore.Mvc;
using Tienda_Online.Backend.Clases;
using Tienda_Online.Shared.Entidades;

namespace Tienda_Online.Backend.Controllers
{
    [ApiController]
    [Route("/api/carritoCompra")]
    public class CarritoCompraController:ControllerBase
    {
        private clsCarritoCompra _carritoCompra;

        public CarritoCompraController(clsCarritoCompra carritoCompra)
        {
            _carritoCompra = carritoCompra;
        }


        [HttpPost("CrearCarrito")]
        public async Task<IActionResult> CrearCarritoCompra(CarritoDeCompra carrito)
        {
            var _carrito = await _carritoCompra.CrearCarritoCompra(carrito);
            if (_carrito.Exitoso)
            {
                return Ok(_carrito.Respuesta);
            }
            return BadRequest();
        }

        [HttpGet("ObtenerCarritos")]
        public async Task<IActionResult> ObtenerListaCarrito()
        {
            var _carrito = await _carritoCompra.ObtenerListaCarrito();
            if (_carrito.Exitoso)
            {
                return Ok(_carrito.Respuesta);
            }
            return BadRequest();
        }
    }
}
