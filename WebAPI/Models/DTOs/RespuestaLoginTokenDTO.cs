using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTOs
	{	 
	public class RespuestaLoginTokenDTO
		{
		[Required]
		public string Token { get; set; } = string.Empty;
		}
	}
