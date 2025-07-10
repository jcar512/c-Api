using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Data;
using System.Security.Claims;

namespace WebAPI.Controllers
	{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class EspectaculoController : ControllerBase
		{
		private readonly AppDbContext _context;

		public EspectaculoController(AppDbContext context)
			{
			_context = context;
			}

		[HttpGet]
		public ActionResult<List<RespuestaEspectaculoDTO>> GetEspectaculos()
			{
			List<Espectaculo> espectaculos = [.. _context.Espectaculos
				.Include(espectaculo => espectaculo.Artista)
				.Include(espectaculo => espectaculo.Usuario)];

			List<RespuestaEspectaculoDTO> respuestaEspectaculos = [];

			foreach (Espectaculo espectaculo in espectaculos)
				{
				RespuestaEspectaculoDTO respuestaEspectaculo = new()
					{
					Id = espectaculo.Id,
					Nombre = espectaculo.Nombre,
					Fecha = espectaculo.Fecha.ToString("yyyy-MM-dd"),
					ArtistaId = espectaculo.ArtistaId,
					NombreArtista = espectaculo.Artista.Nombre,
					UsuarioId = espectaculo.UsuarioId,
					};

				respuestaEspectaculos.Add(respuestaEspectaculo);
				}

			if (espectaculos.Count ==  0)
				{
				return NotFound("No se encontro ningun espectaculo");
				}

			return respuestaEspectaculos;
			}


		[HttpGet("{id}")]
		public ActionResult<RespuestaEspectaculoDTO> GetEspectaculo(int id)
			{
			if (id <= 0)
				{
				return BadRequest("Id no puede ser menor o igual a cero");
				}

			Espectaculo? espectaculo = _context.Espectaculos
				.Include(espectaculo => espectaculo.Artista)
				.Include(espectaculo => espectaculo.Usuario)
				.FirstOrDefault(espectaculo => espectaculo.Id == id);

			if (espectaculo == null)
				{
				return BadRequest($"Espectaculo con Id ({id}) no fue encontrado");
				}

			RespuestaEspectaculoDTO respuestaEspectaculo = new ()
				{
				Id = espectaculo.Id,
				Nombre = espectaculo.Nombre,
				Fecha = espectaculo.Fecha.ToString("yyyy-MM-dd"),
				ArtistaId = espectaculo.ArtistaId,
				NombreArtista = espectaculo.Artista!.Nombre,
				UsuarioId = espectaculo.UsuarioId,
				};

			return Ok(respuestaEspectaculo);
			}

		[HttpPost]
		public ActionResult<RespuestaEspectaculoDTO> PostEspectaculo([FromBody] EspectaculoDTO parametros)
			{
			string? usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (usuarioId == null || usuarioId == string.Empty)
				return Unauthorized("No se pudo obtener el Id del usuario autenticado");

			if (parametros == null)
				{
				return BadRequest("El cuerpo del request esta vacio");
				}

			if (ModelState.IsValid == false)
				{
				return BadRequest(ModelState);
				}

			Artista? artista = _context.Artistas
				.FirstOrDefault(artista => artista.Id == parametros.ArtistaId);

			if (artista == null)
				{
				return BadRequest("El artista no existe");
				}

			Espectaculo? espectaculo = _context.Espectaculos
				.FirstOrDefault(espectaculo => espectaculo.Nombre.ToLower() == parametros.Nombre.ToLower());

			if (espectaculo != null)
				{
				return BadRequest("Ya existe un espectaculo con ese nombre");
				}

			Espectaculo espectaculoNuevo = new()
				{
				Nombre = parametros.Nombre,
				Fecha = DateOnly.Parse(parametros.Fecha),
				ArtistaId = parametros.ArtistaId,
				UsuarioId = Int32.Parse(usuarioId),
				};

			_context.Espectaculos.Add(espectaculoNuevo);

			try
				{
				_context.SaveChanges();

				RespuestaEspectaculoDTO respuestaEspectaculo = new()
					{
					Id = espectaculoNuevo.Id,
					Nombre = espectaculoNuevo.Nombre,
					Fecha = espectaculoNuevo.Fecha.ToString("yyyy-MM-dd"),
					ArtistaId = espectaculoNuevo.ArtistaId,
					NombreArtista = artista.Nombre,
					UsuarioId = Int32.Parse(usuarioId),
					};

				return Ok(respuestaEspectaculo);
				}
			catch (Exception ex)
				{
				return BadRequest(ex.Message);
				}
			}

		[HttpPut]
		public ActionResult<RespuestaEspectaculoDTO> PutEspectaculo(int id, [FromBody] EspectaculoDTO parametros)
			{
			if (parametros == null)
				{
				return BadRequest("El cuerpo del request esta vacio");
				}

			Espectaculo? espectaculo = _context.Espectaculos.FirstOrDefault(espectaculos => espectaculos.Id == id);

			if (espectaculo == null)
				return NotFound("No existe un espectaculo con ese Id");

			if (ModelState.IsValid == false)
				return BadRequest(ModelState);

			Artista? artista = _context.Artistas
				.FirstOrDefault(artista => artista.Id == parametros.ArtistaId);

			if (artista == null)
				{
				return BadRequest("El artista no existe");
				}

			Espectaculo? espectaculoPorNombre = _context.Espectaculos
			.FirstOrDefault(espectaculo => espectaculo.Nombre.ToLower() == parametros.Nombre.ToLower());

			if (espectaculoPorNombre != null)
				{
				return BadRequest("Ya existe un espectaculo con ese nombre");
				}

			espectaculo.Nombre = parametros.Nombre;
			espectaculo.Fecha = DateOnly.Parse(parametros.Fecha);
			espectaculo.ArtistaId = parametros.ArtistaId;

			try
				{
				_context.Espectaculos.Update(espectaculo);
				_context.SaveChanges();

				RespuestaEspectaculoDTO respuestaEspectaculo = new()
					{
					Id = espectaculo.Id,
					Nombre = espectaculo.Nombre,
					Fecha = espectaculo.Fecha.ToString("yyyy-MM-dd"),
					ArtistaId = espectaculo.ArtistaId,
					};

				return Ok(respuestaEspectaculo);
				}
			catch (Exception ex)
				{
				return BadRequest(ex.Message);
				}
			}


		[HttpDelete]
		public ActionResult<bool> DeleteEspectaculo(int id)
			{
			if(id <= 0)
				{
				return BadRequest("Es necesario que ingrese un Id valido");
				}

			Espectaculo? espectaculo = _context.Espectaculos
				.FirstOrDefault(espectaculo => espectaculo.Id == id);

			if (espectaculo == null)
				{
				return BadRequest("Espectaculo no encontrado");
				}

			try
				{
				_context.Espectaculos.Remove(espectaculo);
				_context.SaveChanges();

				return Ok(true);
				}
			catch (Exception ex)
				{
				return BadRequest(ex.Message);
				}
			}
		}
	}
