using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTOs
	{
	public class EspectaculoDTO
		{
		[Required]
		public string Nombre { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Date)]
		public string Fecha { get; set; } = string.Empty;

		[Required]
		public int ArtistaId { get; set; } = 0;
		}
	}
