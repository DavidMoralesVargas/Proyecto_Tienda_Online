using Microsoft.AspNetCore.Mvc;
using Tienda_Online.Backend.Clases;
using Tienda_Online.Shared.DTOs;

namespace Tienda_Online.Backend.Controllers
{
    [ApiController]
    [Route("/api/informes")]
    public class InformesController:ControllerBase
    {
        private readonly clsInforme _informe;

        public InformesController(clsInforme informe)
        {
            _informe = informe;
        }

        [HttpPost("GuardarInforme")]
        public async Task<IActionResult> GuardarInforme(List<CarritoConProductoDTO> carritos)
        {
            var informe = await _informe.GuardarInforme(carritos);
            if (informe.Exitoso)
            {
                return Ok(informe.Respuesta);
            }
            return BadRequest(informe.Mensaje);
        }
    }
}
