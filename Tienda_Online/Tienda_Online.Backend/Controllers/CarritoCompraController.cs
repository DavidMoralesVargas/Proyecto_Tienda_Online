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

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Actualizar(CarritoDeCompra carrito)
        {
            var _carrito = await _carritoCompra.Actualizar(carrito);
            if(_carrito.Exitoso)
            {
                return Ok(_carrito.Respuesta);
            }
            return BadRequest(_carrito.Mensaje);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarCarritoCompra(int id)
        {
            var _carrito = await _carritoCompra.BuscarCarritoCompra(id);
            if (_carrito.Exitoso)
            {
                return Ok(_carrito.Respuesta);
            }
            return BadRequest(_carrito.Mensaje);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var _carrito = await _carritoCompra.EliminarProducto(id);
            if(_carrito.Exitoso)
            {
                return NoContent();
            }
            return BadRequest(_carrito.Mensaje);
        }
    }
}
