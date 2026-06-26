using Hospital.Application.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    /// <summary>
    /// Gestiona la autenticación de usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Autentica un usuario.
        /// </summary>
        /// <remarks>
        /// Valida las credenciales del usuario y devuelve un token JWT
        /// que deberá enviarse en el encabezado Authorization para acceder
        /// a los endpoints protegidos.
        ///
        /// Ejemplo:
        ///
        /// POST /api/Auth/login
        ///
        /// {
        ///   "username": "admin",
        ///   "password": "123456"
        /// }
        /// </remarks>
        /// <param name="cmd">Credenciales del usuario.</param>
        /// <returns>Token JWT.</returns>
        /// <response code="200">Autenticación exitosa.</response>
        /// <response code="401">Credenciales inválidas.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginCommand cmd)
        {
            var result = await _sender.Send(cmd);
            return Ok(result);
        }
    }
}