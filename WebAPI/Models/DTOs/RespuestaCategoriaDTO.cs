namespace WebAPI.Models.DTOs
	{
	public class RespuestaCategoriaDTO
		{	 
		public int Id { get; set; }
		public string Nombre { get; set; } = string.Empty;
		public string Descripcion { get; set; }	= string.Empty;

		public RespuestaCategoriaDTO() { }

		public RespuestaCategoriaDTO(int id, string nombre, string descripcion)
			{
			Id = id;
			Nombre = nombre;
			Descripcion = descripcion;
			}
		}
	}
