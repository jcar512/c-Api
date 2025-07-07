namespace WebAPI.Models.DTOs
{
	public class RespuestaEspectaculoDTO
		{
		public int Id { get; set; } = 0;
		public string Nombre { get; set; } = string.Empty;
		public DateOnly Fecha { get; set; }
		}
}
