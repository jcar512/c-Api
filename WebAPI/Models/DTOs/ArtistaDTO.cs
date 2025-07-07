using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTOs
	{
	public class ArtistaDTO
		{
		[Required]
		public string Nombre { get; set; } = string.Empty;
		public string? Genero { get; set; }
		[Required]
		[DataType(DataType.Date)]
		public string FechaNacimiento { get; set; } = string.Empty;
		public string? Nacionalidad { get; set; }
		[Required]
		public int CategoriaId { get; set; } = 0;
		}
	}
