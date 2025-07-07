using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
	{
	public class AppDbContext : DbContext
		{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
			{
			
			}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
			modelBuilder.Entity<Artista>().ToTable("Artistas");
			modelBuilder.Entity<Categoria>().ToTable("Categorias");
			modelBuilder.Entity<Usuario>().ToTable("Usuarios");
			modelBuilder.Entity<Espectaculo>().ToTable("Espectaculos");
			modelBuilder.Entity<ArtistasEspectaculo>().ToTable("ArtistasEspectaculos");

			modelBuilder.Entity<Categoria>()
				.HasMany(categoria => categoria.Artistas)
				.WithOne(artista => artista.Categoria)
				.HasForeignKey(artista => artista.CategoriaId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Artista>()
				.HasOne(artista => artista.Categoria)
				.WithMany(categoria => categoria.Artistas)
				.HasForeignKey(artista => artista.CategoriaId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Usuario>()
				.HasMany(usuario => usuario.Artistas)
				.WithOne(artista => artista.Usuario)
				.HasForeignKey(artista => artista.UsuarioId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ArtistasEspectaculo>()
				.HasKey(artistaEspectaculo => new { artistaEspectaculo.ArtistaId, artistaEspectaculo.EspectaculoId });

			modelBuilder.Entity<ArtistasEspectaculo>()
				.HasOne(artistaEspectaculo => artistaEspectaculo.Artista)
				.WithMany(artista => artista.ArtistasEspectaculo)
				.HasForeignKey(artistaEspectaculo => artistaEspectaculo.ArtistaId);

			modelBuilder.Entity<ArtistasEspectaculo>()
				.HasOne(artistaEspectaculo => artistaEspectaculo.Espectaculo)
				.WithMany(espectaculo => espectaculo.ArtistasEspectaculo)
				.HasForeignKey(artistaEspectaculo => artistaEspectaculo.EspectaculoId);

			modelBuilder.Entity<Artista>()
				.HasOne(artista => artista.Usuario)
				.WithMany(usuario => usuario.Artistas)
				.HasForeignKey(artista => artista.UsuarioId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Artista>()
				.HasIndex(artista => artista.Nombre)
				.IsUnique();

			modelBuilder.Entity<Categoria>()
				.HasIndex(categoria => categoria.Nombre)
				.IsUnique();

			modelBuilder.Entity<Usuario>()
				.HasIndex(usuario => usuario.Email)
				.IsUnique();
			}

		public DbSet<Artista> Artistas { get; set; }
		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Espectaculo> Espectaculos { get; set; }
		public DbSet<ArtistasEspectaculo> ArtistasEspectaculos { get; set; }
		}
	}
