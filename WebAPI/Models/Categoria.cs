using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
	{
	public class Categoria
		{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string? Nombre { get; set; }
		public string? Descripcion { get; set; }
		public List<Artista>? Artistas { get; set; } = new List<Artista>();

		public Categoria() { }

		public Categoria(string nombre, string descripcion)
			{
			Nombre = nombre;
			Descripcion = descripcion;
			}
		}
	}
