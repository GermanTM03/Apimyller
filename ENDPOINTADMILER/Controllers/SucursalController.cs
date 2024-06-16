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
    [Route("api/Branch")]
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
        [Route("Branchs")]
        public async Task<IActionResult> Sucursales()
        {
            // Obtener el ID de usuario del token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = "ID de usuario no encontrado en el token." });
            }

            // Obtener las sucursales asociadas al usuario
            var sucursales = await _admylerContext.Branchs
                .Where(s => s.PkUser == int.Parse(userId))
                .ToListAsync();

            return StatusCode(StatusCodes.Status200OK, new { value = sucursales });

        }

        [HttpPost]
        [Route("AddBranchs")]
        public async Task<IActionResult> AgregarSucursal(Branch nuevaSucursal)
        {
            // Obtener el ID de usuario del token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = "ID de usuario no encontrado en el token." });
            }

            // Asignar el valor del PKUsuario al campo Fkusuario de la nueva sucursal
            nuevaSucursal.PkUser = int.Parse(userId);

            try
            {
                // Agregar la nueva sucursal a la base de datos
                _admylerContext.Branchs.Add(nuevaSucursal);
                await _admylerContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { success = true, message = "Sucursal agregada exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"Error al agregar la sucursal: {ex.Message}" });
            }
        }


        [HttpDelete]
        [Route("DeleteBranchs/{id}")]
        public async Task<IActionResult> EliminarSucursal(int id)
        {
            // Obtener el ID de usuario del token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = "ID de usuario no encontrado en el token." });
            }

            // Buscar la sucursal en la base de datos
            var sucursal = await _admylerContext.Branchs.FindAsync(id);

            if (sucursal == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { error = "Sucursal no encontrada." });
            }

            // Verificar si la sucursal le pertenece al usuario logueado
            if (sucursal.PkUser != int.Parse(userId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { error = "No tienes permisos para eliminar esta sucursal." });
            }

            try
            {
                // Eliminar la sucursal de la base de datos
                _admylerContext.Branchs.Remove(sucursal);
                await _admylerContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { success = true, message = "Sucursal eliminada exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"Error al eliminar la sucursal: {ex.Message}" });
            }
        }
        [HttpPut]
        [Route("EditBranchs/{id}")]
        public async Task<IActionResult> EditarSucursal(int id, [FromBody] Branch sucursalEditada)
        {
            // Obtener el ID de usuario del token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = "ID de usuario no encontrado en el token." });
            }

            // Buscar la sucursal en la base de datos usando el ID de la ruta
            var sucursal = await _admylerContext.Branchs.FindAsync(id);

            if (sucursal == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { error = "Sucursal no encontrada." });
            }

            // Verificar si la sucursal le pertenece al usuario logueado
            if (sucursal.PkUser != int.Parse(userId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { error = "No tienes permisos para editar esta sucursal." });
            }

            try
            {
                // Actualizar los campos de la sucursal
                sucursal.BusinessName = sucursalEditada.BusinessName;
                sucursal.Address = sucursalEditada.Address;
                sucursal.RFC = sucursalEditada.RFC;
                sucursal.Email = sucursalEditada.Email;
                sucursal.PhoneNumber = sucursalEditada.PhoneNumber;

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
        [Route("AddProduct/{Id}")]
        public async Task<IActionResult> AgregarProductoInventario(int Id, Inventory nuevoProducto)
        {
            // Obtener el ID de usuario del token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = "ID de usuario no encontrado en el token." });
            }

            // Verificar si el usuario tiene acceso a la sucursal
            var sucursal = await _admylerContext.Branchs.FindAsync(Id);
            if (sucursal == null || sucursal.PkUser != int.Parse(userId))
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { error = "No tienes permisos para agregar productos a esta sucursal." });
            }

            try
            {
                // Asignar la sucursal al nuevo producto
                nuevoProducto.BranchId = Id;

                // Agregar el nuevo producto al inventario
                _admylerContext.Inventorys.Add(nuevoProducto);
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
