using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ENDPOINTADMILER.Custom;
using ENDPOINTADMILER.Models;
using ENDPOINTADMILER.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ENDPOINTADMILER.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly AdmylerContext _admylerContext;
        private readonly Utilidades _utilidades;
        public AccesoController(AdmylerContext admylerContext, Utilidades utilidades)
        {
            _admylerContext = admylerContext;
            _utilidades = utilidades;
            
        }

        [HttpPost]
        [Route("RegistroUser")]
        public async Task<IActionResult>RegistroUser(UsuarioDTO objeto)
        {
            var modeloUsuario = new Usuario
            {
                Nombre= objeto.Nombre,
                ApellidoP = objeto.ApellidoP,
                ApellidoM = objeto.ApellidoM,
                Correo = objeto.Correo,
                Contra = _utilidades.encriptarSHA256(objeto.Contra)
            };
            await _admylerContext.Usuarios.AddAsync(modeloUsuario);
            await _admylerContext.SaveChangesAsync();

            if (modeloUsuario.Pkusuario != 0)
                return StatusCode(StatusCodes.Status200OK, new { IsSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { IsSuccess = false });

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioEncontrado = await _admylerContext.Usuarios
                .Where(
                u =>
                u.Correo == objeto.Correo &&
                u.Contra == _utilidades.encriptarSHA256(objeto.Contra))
                .FirstOrDefaultAsync();

            if(usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK,new { IsSuccess = false,token="" });
            else
                return StatusCode(StatusCodes.Status200OK, new { IsSuccess = true,PKUsuario = usuarioEncontrado.Pkusuario, token = _utilidades.generarJWT(usuarioEncontrado)});

        }


    }
}
