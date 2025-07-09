using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Helpers;
using WebAPI.Models.DTOs;

namespace WebAPI.Controllers
	{
	[Route("api/[controller]")]
	[ApiController]
	public class UsuariosController : ControllerBase
		{
		private readonly AppDbContext _context;
		private readonly Authenticate _authenticate;

		public UsuariosController(AppDbContext context, Authenticate autentica)
			{
			_context = context;
			_authenticate = autentica;
			}

		[HttpPost]
		public ActionResult<Usuario> PostUsuario([FromBody] RegistroUsuarioDTO registroUsuarioDTO)
			{
			if (ModelState.IsValid == false)
				return BadRequest(ModelState);

			Usuario? usuario = _context.Usuarios.FirstOrDefault(u => u.Email == registroUsuarioDTO.Email);

			if (usuario != null)
				return BadRequest("Ya existe un usuario con ese email");

			usuario = new Usuario(
				registroUsuarioDTO.Email!,
				registroUsuarioDTO.Password!
			);

			_context.Usuarios.Add(usuario);

			try
				{
				_context.SaveChanges();
				return Ok(usuario);
				}
			catch (Exception ex)
				{
				return BadRequest(ex.Message);
				}
			}

		[HttpPost("login")]
		public ActionResult<Usuario> PostLogin([FromBody] LoginUsuarioDTO loginUsuarioDTO)
			{
			if (ModelState.IsValid == false)
				return BadRequest(ModelState);

			Usuario? usuario = _context.Usuarios.FirstOrDefault(u => u.Email == loginUsuarioDTO.Email);
			string passwordEncriptadoParametro = Usuario.EncriptarPassword(loginUsuarioDTO.Password);

			if (usuario == null || usuario.PasswordEncriptado != passwordEncriptadoParametro)
				return Unauthorized("Usuario incorrecto o contraseña incorrecta");

			RespuestaLoginTokenDTO token = new RespuestaLoginTokenDTO();
			token.Token = _authenticate.CrearToken(usuario);
			return Ok(token);
			}
		}
	}
