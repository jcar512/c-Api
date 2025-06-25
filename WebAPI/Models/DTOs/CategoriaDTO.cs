using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTOs
	{
	public class CategoriaDTO
		{
		[Required]
		public string? Nombre { get; set; }
		public string? Descripcion { get; set; }
		}
	}
