namespace WebAPI.Models.DTOs
	{
	public class RespuestaArtistaDTO
		{
		public int Id { get; set; } = 0;
		public string Nombre { get; set; } = string.Empty;
		public string Genero { get; set; } = string.Empty;
		public string Nacionalidad { get; set; } = string.Empty;
		public string FechaNacimiento { get; set; } = string.Empty;
		public string CategoriaNombre { get; set; } = string.Empty;
		public int CategoriaId { get; set; } = 0;
		public string UsuarioEmail { get; set; } = string.Empty;
		public int UsuarioId { get; set; } = 0;

		public RespuestaArtistaDTO() { }

		public RespuestaArtistaDTO(int id, string nombre, string genero, string nacionalidad, string fechaNacimiento, string categoriaNombre, int categoriaId, string usuarioEmail, int usuarioId)
			{
			Id = id;
			Nombre = nombre;
			Genero = genero;
			Nacionalidad = nacionalidad;
			FechaNacimiento = fechaNacimiento;
			CategoriaNombre = categoriaNombre;
			CategoriaId = categoriaId;
			UsuarioEmail = usuarioEmail;
			UsuarioId = usuarioId;
			}
		}
	}
