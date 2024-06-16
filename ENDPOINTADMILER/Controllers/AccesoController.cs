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
        [Route("RegisterUser")]
        public async Task<IActionResult>RegistroUser(UserDTO objeto)
        {
            var modeloUsuario = new User
            {
                FullName= objeto.Full_Name,
                Email = objeto.Email,
                Password = _utilidades.encriptarSHA256(objeto.Password)
            };
            await _admylerContext.Users.AddAsync(modeloUsuario);
            await _admylerContext.SaveChangesAsync();

            if (modeloUsuario.PkUser != 0)
                return StatusCode(StatusCodes.Status200OK, new { IsSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { IsSuccess = false });

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioEncontrado = await _admylerContext.Users
                .Where(
                u =>
                u.Email == objeto.Correo &&
                u.Password == _utilidades.encriptarSHA256(objeto.Contra))
                .FirstOrDefaultAsync();

            if(usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK,new { IsSuccess = false,token="" });
            else
                return StatusCode(StatusCodes.Status200OK, new { IsSuccess = true,PKUsuario = usuarioEncontrado.PkUser, token = _utilidades.generarJWT(usuarioEncontrado)});

        }


    }
}
