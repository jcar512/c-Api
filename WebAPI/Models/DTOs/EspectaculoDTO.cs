using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTOs
	{
	public class EspectaculoDTO
		{
		[Required]
		public string Nombre { get; set; } = string.Empty;
		[Required]
		public DateOnly Fecha { get; set; }
		}
	}
