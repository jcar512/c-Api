using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTOs
{
	public class RespuestaEspectaculoDTO
		{
		public int Id { get; set; }
		public string Nombre { get; set; } = string.Empty;	  		
		public DateOnly Fecha { get; set; }			  		
		public int ArtistaId { get; set; }
		public RespuestaEspectaculoDTO() { }
		public RespuestaEspectaculoDTO(int id, string nombre, DateOnly fecha, int artistaId)
			{
			Id = id;
			Nombre = nombre;
			Fecha = fecha;
			ArtistaId = artistaId;
			}
		}
	}
