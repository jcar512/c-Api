﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
	{
	public class Espectaculo
		{

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Nombre { get; set; } = string.Empty;	  		
		public DateOnly Fecha { get; set; }
		[Required]
		public int ArtistaId { get; set; }
		public Artista? Artista { get; set; }

		public int UsuarioId { get; set; }
		public Usuario? Usuario { get; set; }
		
		public Espectaculo() { }
		public Espectaculo(string nombre, DateOnly fecha, int artistaId, int usuarioId)
			{
			Nombre = nombre;
			Fecha = fecha;
			ArtistaId = artistaId;
			UsuarioId = usuarioId;
			}
		}
	}
