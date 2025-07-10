using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTOs
{
	public class RespuestaEspectaculoDTO
		{
		public int Id { get; set; } = 0;
		public string Nombre { get; set; } = string.Empty;
		public string Fecha { get; set; } = string.Empty;
		public int ArtistaId { get; set; } = 0;
		public string NombreArtista { get; set; } = string.Empty;

		public int UsuarioId { get; set; } = 0;
		public RespuestaEspectaculoDTO() { }
		public RespuestaEspectaculoDTO(int id, string nombre, string fecha, int artistaId, string nombreArtista, int usuarioId)
			{
			Id = id;
			Nombre = nombre;
			Fecha = fecha;
			ArtistaId = artistaId;
			NombreArtista = nombreArtista;
			UsuarioId = usuarioId;
			}
		}
	}
