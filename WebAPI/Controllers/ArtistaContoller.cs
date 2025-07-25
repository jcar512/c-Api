﻿using Microsoft.AspNetCore.Mvc;
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
	public class ArtistaController : ControllerBase
		{
		private readonly AppDbContext _context;

		public ArtistaController(AppDbContext context)
			{
			_context = context;
			}

		// GET: api/Artistas
		[HttpGet]
		public ActionResult<List<RespuestaArtistaDTO>> GetArtistas()
			{
			List<Artista> artistas = [.. _context.Artistas
				.Include(artista => artista.Categoria)
				.Include(artista => artista.Usuario)];   

			List<RespuestaArtistaDTO> respuestaArtistas = [];

			foreach (Artista artista in artistas)
				{
				RespuestaArtistaDTO respuestaArtista = new ()
				{
				Id = artista.Id,
				Nombre = artista.Nombre,
				Genero = artista.Genero ?? string.Empty,
				FechaNacimiento = artista.FechaNacimiento.ToString("yyyy-MM-dd"),
				Nacionalidad = artista.Nacionalidad ?? string.Empty,
				CategoriaNombre = artista.Categoria!.Nombre ?? string.Empty,
				CategoriaId = artista.CategoriaId ?? 0,
				UsuarioEmail = artista.Usuario.Email,
				UsuarioId = artista.Usuario.Id
				};

				respuestaArtistas.Add(respuestaArtista);
				}

			if (artistas.Count == 0)
				{
				return NotFound("No se encontro ningun artista");
				}

			return respuestaArtistas;
			}

		// GET: api/Artistas/5
		[HttpGet("{id}")]
		public ActionResult<RespuestaArtistaDTO> GetArtista(int id)
			{
			if (id <= 0)
				return BadRequest("Id no puede ser menor o igual a cero");

			Artista? artista = _context.Artistas
				.Include(artista => artista.Categoria)
				.Include(artista => artista.Usuario)
				.FirstOrDefault(artista => artista.Id == id);

			if (artista == null)
				return NotFound($"Artista con Id ({id}) no fue encontrado");

			RespuestaArtistaDTO respuestaArtista = new ()
				{
				Id = artista.Id,
				Nombre = artista.Nombre,
				Genero = artista.Genero ?? string.Empty,
				FechaNacimiento = artista.FechaNacimiento.ToString("yyyy-MM-dd"),
				Nacionalidad = artista.Nacionalidad ?? string.Empty,
				CategoriaNombre = artista.Categoria!.Nombre ?? string.Empty,
				CategoriaId = artista.CategoriaId ?? 0,
				UsuarioEmail = artista.Usuario!.Email,
				UsuarioId = artista.Usuario.Id
				};

			return Ok(respuestaArtista);
			}


		// POST: api/Artistas
		[HttpPost]
		public ActionResult<RespuestaArtistaDTO> PostArtista([FromBody] ArtistaDTO parametrosArtista)
			{
			string? usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (usuarioId == null || usuarioId == string.Empty)
				return Unauthorized("No se pudo obtener el Id del usuario autenticado");

			if (parametrosArtista == null)
				return BadRequest("El cuerpo del request estaba vacio");

			if (ModelState.IsValid == false)
				return BadRequest(ModelState);

			Usuario? usuario = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == usuarioId);

			if (usuario == null)
				return Unauthorized("Usuario no encontrado");

			Categoria? categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == parametrosArtista.CategoriaId);

			if (categoria == null)
				return BadRequest("La categoria no existe");

			Artista? artista = _context.Artistas.FirstOrDefault(artista => artista.Nombre == parametrosArtista.Nombre);

			if (artista != null)
				return BadRequest("Ya existe un Artista con ese nombre");

			artista = new Artista(
				parametrosArtista.Nombre,
				parametrosArtista.Genero!,
				DateOnly.Parse(parametrosArtista.FechaNacimiento),
				parametrosArtista.Nacionalidad!,
				parametrosArtista.CategoriaId,
				usuario.Id
			);

			_context.Artistas.Add(artista);

			try
				{
				_context.SaveChanges();

				RespuestaArtistaDTO respuestaArtista = new ()
					{
					Id = artista.Id,
					Nombre = artista.Nombre,
					Genero = artista.Genero ?? string.Empty,
					FechaNacimiento = artista.FechaNacimiento.ToString("yyyy-MM-dd"),
					Nacionalidad = artista.Nacionalidad ?? string.Empty,
					CategoriaNombre = categoria.Nombre ?? string.Empty,
					CategoriaId = artista.CategoriaId ?? 0,
					UsuarioEmail = usuario.Email,
					UsuarioId = usuario.Id
					};

				return Ok(respuestaArtista);
				}
			catch (Exception ex)
				{
				return BadRequest(ex.Message);
				}
			}

		// PUT: api/Artistas/5
		[HttpPut("{id}")]
		public ActionResult<RespuestaArtistaDTO> PutArtista(int id, [FromBody] ArtistaDTO parametrosArtista)
			{
			string? usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (usuarioId == null || usuarioId == string.Empty)
				return Unauthorized("No se pudo obtener el Id del usuario autenticado");

			if (parametrosArtista == null)
				return BadRequest("El cuerpo del request estaba vacio");

			Artista? artista = _context.Artistas.FirstOrDefault(artista => artista.Id == id);

			if (artista == null)
				return NotFound("No existe un artista con ese Id");

			if (ModelState.IsValid == false)
				return BadRequest(ModelState);

			Usuario? usuario = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == usuarioId);

			if (usuario == null)
				return Unauthorized("Usuario no encontrado");

			Categoria? categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == parametrosArtista.CategoriaId);

			if (categoria == null)
				return BadRequest("La categoria no existe");

			Artista? artistaPorNombre = _context.Artistas
				.FirstOrDefault(artista => artista.Nombre == parametrosArtista.Nombre);

			if (artistaPorNombre != null)
				return BadRequest("Ya existe un Artista con ese nombre");

			artista.Nombre = parametrosArtista.Nombre;
			artista.Genero = parametrosArtista.Genero;
			artista.FechaNacimiento = DateOnly.Parse(parametrosArtista.FechaNacimiento);
			artista.Nacionalidad = parametrosArtista.Nacionalidad;
			artista.CategoriaId = parametrosArtista.CategoriaId;

			try
				{
				_context.Artistas.Update(artista);
				_context.SaveChanges();

				RespuestaArtistaDTO respuestaArtista = new RespuestaArtistaDTO()
					{
					Id = artista.Id,
					Nombre = artista.Nombre,
					Genero = artista.Genero ?? string.Empty,
					FechaNacimiento = artista.FechaNacimiento.ToString("yyyy-MM-dd"),
					Nacionalidad = artista.Nacionalidad ?? string.Empty,
					CategoriaNombre = categoria.Nombre ?? string.Empty,
					CategoriaId = artista.CategoriaId ?? 0,
					UsuarioEmail = usuario.Email,
					UsuarioId = usuario.Id
					};

				return Ok(respuestaArtista);
				}
			catch (Exception ex)
				{
				return BadRequest(ex.Message);
				}
			}

		// DELETE: api/Artistas/5
		[HttpDelete("{id}")]
		public ActionResult<bool> DeleteArtista(int id)
			{
			if (id <= 0)
				return BadRequest("Es necesario un Id");

			Artista? artista = _context.Artistas
				.FirstOrDefault(artista => artista.Id == id);

			if (artista == null)
				return NotFound("Artista no encontrado");

			try
				{
				_context.Artistas.Remove(artista);
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