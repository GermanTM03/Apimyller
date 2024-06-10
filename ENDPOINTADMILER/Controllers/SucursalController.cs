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


        [HttpDelete]
        [Route("EliminarSucursal/{id}")]
        public async Task<IActionResult> EliminarSucursal(int id)
        {
            // Obtener el ID de usuario del token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = "ID de usuario no encontrado en el token." });
            }

            // Buscar la sucursal en la base de datos
            var sucursal = await _admylerContext.Sucursals.FindAsync(id);

            if (sucursal == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { error = "Sucursal no encontrada." });
            }

            // Verificar si la sucursal le pertenece al usuario logueado
            if (sucursal.Fkusuario != int.Parse(userId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { error = "No tienes permisos para eliminar esta sucursal." });
            }

            try
            {
                // Eliminar la sucursal de la base de datos
                _admylerContext.Sucursals.Remove(sucursal);
                await _admylerContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { success = true, message = "Sucursal eliminada exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"Error al eliminar la sucursal: {ex.Message}" });
            }
        }
        [HttpPut]
        [Route("EditarSucursal/{id}")]
        public async Task<IActionResult> EditarSucursal(int id, [FromBody] Sucursal sucursalEditada)
        {
            // Obtener el ID de usuario del token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = "ID de usuario no encontrado en el token." });
            }

            // Buscar la sucursal en la base de datos usando el ID de la ruta
            var sucursal = await _admylerContext.Sucursals.FindAsync(id);

            if (sucursal == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { error = "Sucursal no encontrada." });
            }

            // Verificar si la sucursal le pertenece al usuario logueado
            if (sucursal.Fkusuario != int.Parse(userId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { error = "No tienes permisos para editar esta sucursal." });
            }

            try
            {
                // Actualizar los campos de la sucursal
                sucursal.NombreNegocio = sucursalEditada.NombreNegocio;
                sucursal.Direccion = sucursalEditada.Direccion;
                sucursal.Rfc = sucursalEditada.Rfc;
                sucursal.CorreoS = sucursalEditada.CorreoS;
                sucursal.NumeroTelefono = sucursalEditada.NumeroTelefono;

                // Otros campos a actualizar...

                await _admylerContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { success = true, message = "Sucursal editada exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"Error al editar la sucursal: {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("AgregarProducto/{sucursalId}")]
        public async Task<IActionResult> AgregarProductoInventario(int sucursalId, Inventario nuevoProducto)
        {
            // Obtener el ID de usuario del token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = "ID de usuario no encontrado en el token." });
            }

            // Verificar si el usuario tiene acceso a la sucursal
            var sucursal = await _admylerContext.Sucursals.FindAsync(sucursalId);
            if (sucursal == null || sucursal.Fkusuario != int.Parse(userId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { error = "No tienes permisos para agregar productos a esta sucursal." });
            }

            try
            {
                // Asignar la sucursal al nuevo producto
                nuevoProducto.Fksucursal = sucursalId;

                // Agregar el nuevo producto al inventario
                _admylerContext.Inventarios.Add(nuevoProducto);
                await _admylerContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { success = true, message = "Producto agregado al inventario exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"Error al agregar el producto al inventario: {ex.Message}" });
            }
        }



    }
}
