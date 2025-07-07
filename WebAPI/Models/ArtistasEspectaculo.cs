namespace WebAPI.Models
	{
	public class ArtistasEspectaculo
		{
		public int ArtistaId { get; set; }
		public Artista Artista { get; set; }
		public int EspectaculoId { get; set; }
		public Espectaculo Espectaculo { get; set; }
		}
	}
