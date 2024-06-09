using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ENDPOINTADMILER.Custom;
using ENDPOINTADMILER.Models;
using ENDPOINTADMILER.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
namespace ENDPOINTADMILER.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly AdmylerContext _admylerContext;

        public SucursalController(AdmylerContext admylerContext)
        {
            _admylerContext = admylerContext;
        }

        [HttpGet]
        [Route("Sucursales")]
        public async Task<IActionResult> Sucursales()
        {
            // Obtener el ID de usuario del token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = "ID de usuario no encontrado en el token." });
            }

            // Obtener las sucursales asociadas al usuario
            var sucursales = await _admylerContext.Sucursals
                .Where(s => s.Fkusuario == int.Parse(userId))
                .ToListAsync();

            return StatusCode(StatusCodes.Status200OK, new { value = sucursales });

        }

        [HttpPost]
        [Route("AgregarSucursal")]
        public async Task<IActionResult> AgregarSucursal(Sucursal nuevaSucursal)
        {
            // Obtener el ID de usuario del token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = "ID de usuario no encontrado en el token." });
            }

            // Asignar el valor del PKUsuario al campo Fkusuario de la nueva sucursal
            nuevaSucursal.Fkusuario = int.Parse(userId);

            try
            {
                // Agregar la nueva sucursal a la base de datos
                _admylerContext.Sucursals.Add(nuevaSucursal);
                await _admylerContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { success = true, message = "Sucursal agregada exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"Error al agregar la sucursal: {ex.Message}" });
            }
        }


    }
}
