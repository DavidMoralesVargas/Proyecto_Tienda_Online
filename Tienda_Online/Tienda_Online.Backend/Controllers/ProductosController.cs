using Microsoft.AspNetCore.Mvc;
using Tienda_Online.Backend.Clases;
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
        public async Task<ActionResult> ObtenerListaProducto()
        {
            var _productos = await _clsProducto.ObtenerListaProducto();
            if (_productos.Exitoso)
            {
                return Ok(_productos.Respuesta);
            }
            return BadRequest();
        }
    }
}
