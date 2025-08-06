using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Core.ListadosCobranza;
using Microsoft.EntityFrameworkCore;

namespace Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;

public partial class ListadosCobranzaDbContext : DbContext
{
	private const string SMALLDATETIME = "smalldatetime";

	public ListadosCobranzaDbContext() { }

	public ListadosCobranzaDbContext(DbContextOptions<ListadosCobranzaDbContext> options) : base(options) { }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(Utils.GetConnectionStrings().Find(i => StringComparer.OrdinalIgnoreCase.Equals(i.Key, Enum.GetName(typeof(DatabaseType), DatabaseType.ListadosCobranza)!)).Value);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
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

		modelBuilder.Entity<CatBonificacionesPrestamo>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_bonificacionesPrestamos");

			entity.Property(e => e.FecCorte)
					.HasColumnType("date")
					.HasColumnName("fec_corte");

			entity.Property(e => e.FecMovimiento)
					.HasColumnType(SMALLDATETIME)
					.HasColumnName("fec_movimiento");

			if (!Utils.IsInTesting())
			{
				entity.Property(e => e.FecMovimiento).HasDefaultValueSql("(CONVERT([smalldatetime],getdate(),(0)))");
			}

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

		modelBuilder.Entity<CatCiudade>(entity =>
		{
			entity.HasNoKey();

			entity.Property(e => e.AntiguedadCiudad).HasColumnType(SMALLDATETIME);

			entity.Property(e => e.FechaUltimaActualizacion).HasColumnType(SMALLDATETIME);

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
		});

		modelBuilder.Entity<CatFactorescartera>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_factorescarteras");

			entity.Property(e => e.ClvLineacreditoespecial).HasColumnName("clv_lineacreditoespecial");

			entity.Property(e => e.ClvPlazo).HasColumnName("clv_plazo");

			entity.Property(e => e.IduCartera).HasColumnName("idu_cartera");

			entity.Property(e => e.PrcFactor)
					.HasColumnType("numeric(18, 2)")
					.HasColumnName("prc_factor");
		});

		modelBuilder.Entity<CatFactorportipoproducto>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_factorportipoproducto");

			entity.Property(e => e.DesTipoproducto)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("des_tipoproducto");

			entity.Property(e => e.FecMovto)
					.HasColumnType("datetime")
					.HasColumnName("fec_movto");

			entity.Property(e => e.IduControl).HasColumnName("idu_control");

			entity.Property(e => e.NumFactor).HasColumnName("num_factor");

			entity.Property(e => e.NumPlazo).HasColumnName("num_plazo");

			entity.Property(e => e.NumTipolineacredito).HasColumnName("num_tipolineacredito");

			entity.Property(e => e.NumTipoproducto).HasColumnName("num_tipoproducto");
		});

		modelBuilder.Entity<CatInteresesMoratorio>(entity =>
		{
			entity.HasKey(e => e.DiasVencidos)
					.HasName("PK_CatInteresesMoratorios_1");

			entity.Property(e => e.DiasVencidos).ValueGeneratedNever();

			entity.Property(e => e.PorcentajeInteresMoratorio)
					.HasMaxLength(8)
					.IsUnicode(false)
					.IsFixedLength();
		});

		modelBuilder.Entity<CatSalariosMinimo>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("catSalariosMinimos");

			entity.Property(e => e.FechaSalario).HasColumnType(SMALLDATETIME);
		});

		OnModelCreatingPartial(modelBuilder);
	}

	public virtual DbSet<CatBonificacionescartera> CatBonificacionescarteras { get; set; } = null!;
	public virtual DbSet<CatBonificacionesPrestamo> CatBonificacionesPrestamos { get; set; } = null!;
	public virtual DbSet<CatCiudade> CatCiudades { get; set; } = null!;
	public virtual DbSet<CatFactorescartera> CatFactorescarteras { get; set; } = null!;
	public virtual DbSet<CatFactorportipoproducto> CatFactorportipoproductos { get; set; } = null!;
	public virtual DbSet<CatInteresesMoratorio> CatInteresesMoratorios { get; set; } = null!;
	public virtual DbSet<CatSalariosMinimo> CatSalariosMinimos { get; set; } = null!;
}
