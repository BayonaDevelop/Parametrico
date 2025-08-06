using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Core.Cat;
using Microsoft.EntityFrameworkCore;
using System;

namespace Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;

public partial class CatDbContext : DbContext
{
	public CatDbContext() { }

	public CatDbContext(DbContextOptions<CatalogosDbContext> options) : base(options) { }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(Utils.GetConnectionStrings().Find(i => StringComparer.OrdinalIgnoreCase.Equals(i.Key, Enum.GetName(typeof(DatabaseType), DatabaseType.CAT)!)).Value);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CatBonificacionesPrestamo>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_bonificacionesPrestamos");

			entity.Property(e => e.FecCorte)
					.HasColumnType("date")
					.HasColumnName("fec_corte");

			entity.Property(e => e.FecMovimiento)
					.HasColumnType("smalldatetime")
					.HasColumnName("fec_movimiento")
					.HasDefaultValueSql("(CONVERT([smalldatetime],getdate(),(0)))");

			entity.Property(e => e.NumDiasTranscurridos).HasColumnName("num_diasTranscurridos");

			entity.Property(e => e.NumPlazo)
					.HasColumnName("num_plazo")
					.HasDefaultValueSql("((0))");

			entity.Property(e => e.PrcBonificacion)
					.HasColumnType("numeric(10, 4)")
					.HasColumnName("prc_bonificacion");

			entity.Property(e => e.PrcBonificacionNueva)
					.HasColumnType("numeric(10, 4)")
					.HasColumnName("prc_bonificacionNueva");
		});

		modelBuilder.Entity<CatBonificacionescartera>(entity =>
		{
			entity.HasKey(e => new { e.IduCartera, e.IduPlazo, e.NumMesestranscurridos, e.NumDiastranscurridos });

			entity.ToTable("cat_bonificacionescarteras");

			entity.Property(e => e.IduCartera).HasColumnName("idu_cartera");

			entity.Property(e => e.IduPlazo).HasColumnName("idu_plazo");

			entity.Property(e => e.NumMesestranscurridos).HasColumnName("num_mesestranscurridos");

			entity.Property(e => e.NumDiastranscurridos).HasColumnName("num_diastranscurridos");

			entity.Property(e => e.PrcBonificacion)
					.HasColumnType("decimal(10, 4)")
					.HasColumnName("prc_bonificacion");
		});

		modelBuilder.Entity<CatInteresesMoratorio>(entity =>
		{
			entity.HasKey(e => e.DiasVencidos);

			entity.ToTable("catInteresesMoratorios");

			entity.Property(e => e.DiasVencidos).ValueGeneratedNever();

			entity.Property(e => e.PorcentajeInteresMoratorio)
					.HasMaxLength(8)
					.IsUnicode(false)
					.IsFixedLength();
		});

		modelBuilder.Entity<Catciudade>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("CATCIUDADES");

			entity.Property(e => e.AntiguedadCiudad).HasColumnType("datetime");

			entity.Property(e => e.FechaUltimaActualizacion).HasColumnType("datetime");

			entity.Property(e => e.GeneraJobCarteraTienda)
					.HasMaxLength(2)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.InicialCiudad)
					.HasMaxLength(4)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.InicialCredito)
					.HasMaxLength(4)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.InicialEstado)
					.HasMaxLength(4)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.IpCiudad)
					.HasMaxLength(15)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.NombreCiudad)
					.HasMaxLength(15)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.PrcCtesbuenos).HasColumnName("prc_ctesbuenos");

			entity.Property(e => e.RegionEstadoDeCuenta)
					.HasMaxLength(2)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.TipoZona)
					.HasMaxLength(1)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.UnIficaCiudadesCobranzas).HasColumnName("UnIFicaCiudadesCobranzas");

			entity.Property(e => e.UnIficaCiudadesInformes).HasColumnName("UnIFicaCiudadesInformes");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	public virtual DbSet<CatBonificacionesPrestamo> CatBonificacionesPrestamos { get; set; } = null!;
	public virtual DbSet<CatBonificacionescartera> CatBonificacionescarteras { get; set; } = null!;
	public virtual DbSet<CatInteresesMoratorio> CatInteresesMoratorios { get; set; } = null!;
	public virtual DbSet<Catciudade> Catciudades { get; set; } = null!;
}
