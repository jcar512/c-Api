using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
	{
	public class Espectaculo
		{	   		
		public int Id { get; set; }
		[Required]
		public string Nombre { get; set; } = string.Empty;	  		
		public DateOnly Fecha { get; set; }	  		
		public List<ArtistasEspectaculo> ArtistasEspectaculo { get; set; }
		public Espectaculo() { }
		public Espectaculo(string nombre, DateOnly fecha)
			{
			Nombre = nombre;
			Fecha = fecha;			
			}
		}
	}
