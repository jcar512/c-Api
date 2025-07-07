using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.DTOs;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriaArtista : ControllerBase
	{
		private readonly AppDbContext _context;

		public CategoriaArtista(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult<List<Categoria>> GetCategorias()
		{
			return _context.Categorias.ToList();
		}


		[HttpGet("{id}")]
		public ActionResult<Categoria> GetCategoria(int id)
		{
			if (id <= 0)
				return BadRequest("Id no puede ser menor o igual a cero");

			Categoria? categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);

			if (categoria == null)
				return NotFound($"Categoria con Id ({id}) no fue encontrada");

			return Ok(categoria);
		}

		// POST: api/Categoria
		[HttpPost]
		public ActionResult<Categoria> PostCategoria([FromBody] CategoriaDTO parametrosCategoria)
		{
			if (parametrosCategoria == null)
				return BadRequest("El cuerpo del request estaba vacio");

			if (ModelState.IsValid == false)
				return BadRequest(ModelState);

			Categoria? categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Nombre == parametrosCategoria.Nombre);

			if (categoria != null)
				return BadRequest("Ya existe una Categoria con ese nombre");

			categoria = new Categoria(
				parametrosCategoria.Nombre!,
				parametrosCategoria.Descripcion!
			);

			_context.Categorias.Add(categoria);

			try
				{
				_context.SaveChanges();
				return Ok(categoria);
				}
			catch (Exception ex)
				{
				return BadRequest(ex.Message);
				}
			}

		// PUT: api/Categoria/5
		[HttpPut("{id}")]
		public ActionResult<Categoria> PutCategoria(int id, [FromBody] CategoriaDTO parametrosCategoria)
			{
			if (parametrosCategoria == null)
				return BadRequest("El cuerpo del request estaba vacio");

			Categoria? categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);

			if (categoria == null)
				return NotFound("No existe una categoria con ese Id");

			if (ModelState.IsValid == false)
				return BadRequest(ModelState);			

			Categoria? categoriaPorNombre = _context.Categorias.FirstOrDefault(categoria => categoria.Nombre == parametrosCategoria.Nombre);

			if (categoriaPorNombre != null)
				return BadRequest("Ya existe una Categoria con ese nombre");

			categoria.Nombre = parametrosCategoria.Nombre;
			categoria.Descripcion = parametrosCategoria.Descripcion;

			try
				{

				_context.Categorias.Update(categoria);
				_context.SaveChanges();
				return Ok(categoria);
				}
			catch (Exception ex)
				{
				return BadRequest(ex.Message);
				}
			}

		[HttpDelete("{id}")]
		public ActionResult<bool> DeleteCategoria(int id)
			{
			if (id <= 0)
				return BadRequest("Es necesario un Id");

			Categoria? categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);

			if (categoria == null)
				return NotFound("Categoria no encontrada");

			try
				{
				_context.Categorias.Remove(categoria);
				return Ok(true);
				}
			catch (Exception ex)
				{
				return BadRequest(ex.Message);
				}
			}
		}
}
