using Microsoft.AspNetCore.Mvc;
using Tienda_Online.Backend.Clases;
using Tienda_Online.Shared.DTOs;
using Tienda_Online.Shared.Entidades;

namespace Tienda_Online.Backend.Controllers
{
    [ApiController]
    [Route("/api/productos")]
    public class ProductosController:ControllerBase
    {
        private readonly clsProducto _clsProducto;

        public ProductosController(clsProducto clsProducto)
        {
            _clsProducto = clsProducto;
        }

        [HttpPost]
        public async Task<ActionResult> CrearProducto(Producto producto)
        {
           var _producto = await _clsProducto.CrearProducto(producto);
            if (_producto.Exitoso)
            {
                return Ok(_producto.Respuesta);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerListaProducto([FromQuery] PaginacionDTO paginacion)
        {
            var _productos = await _clsProducto.ObtenerListaProducto(paginacion);
            if (_productos.Exitoso)
            {
                return Ok(_productos.Respuesta);
            }
            return BadRequest();
        }

        [HttpGet("ObtenerTotalPaginas")]
        public async Task<ActionResult> ObtenerTotalPaginas([FromQuery] PaginacionDTO paginacion)
        {
            var _paginas = await _clsProducto.ObtenerTotalPaginas(paginacion);
            if (_paginas.Exitoso)
            {
                return Ok(_paginas.Respuesta);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> BuscarProducto(int id)
        {
            var _producto = await _clsProducto.BuscarProducto(id);
            if (_producto.Exitoso)
            {
                return Ok(_producto.Respuesta);
            }
            return NotFound();  
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarProducto(Producto producto)
        {
            var _producto = await _clsProducto.ActualizarProducto(producto);
            if (_producto.Exitoso)
            {
                return Ok(producto);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarProducto(int id)
        {
            var _producto = await _clsProducto.EliminarProducto(id);
            if (_producto.Exitoso)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
