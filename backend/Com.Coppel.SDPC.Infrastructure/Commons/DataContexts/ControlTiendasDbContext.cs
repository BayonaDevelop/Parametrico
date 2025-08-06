using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Core.ControlTiendas;
using Microsoft.EntityFrameworkCore;
using System;

namespace Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;

public partial class ControlTiendasDbContext : DbContext
{
	private const string DATETIME = "datetime";

	public ControlTiendasDbContext() { }

	public ControlTiendasDbContext(DbContextOptions<ControlTiendasDbContext> options) : base(options) { }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(Utils.GetConnectionStrings().Find(i => StringComparer.OrdinalIgnoreCase.Equals(i.Key, Enum.GetName(typeof(DatabaseType), DatabaseType.ControlTiendas)!)).Value);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CatCiudade>(entity =>
		{
			entity.HasNoKey();

			entity.Property(e => e.AntiguedadCiudad).HasColumnType(DATETIME);

			entity.Property(e => e.FechaUltimaActualizacion).HasColumnType(DATETIME);

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
					.HasColumnType(DATETIME)
					.HasColumnName("fec_movto");

			entity.Property(e => e.IduControl).HasColumnName("idu_control");

			entity.Property(e => e.NumFactor).HasColumnName("num_factor");

			entity.Property(e => e.NumPlazo).HasColumnName("num_plazo");

			entity.Property(e => e.NumTipolineacredito).HasColumnName("num_tipolineacredito");

			entity.Property(e => e.NumTipoproducto).HasColumnName("num_tipoproducto");
		});

		modelBuilder.Entity<CtlMaestraFecha>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("ctlMaestraFechas");

			entity.Property(e => e.FechaCorte).HasColumnType("smalldatetime");
		});

		modelBuilder.Entity<CatNegocio>(entity =>
		{
			entity.HasNoKey();

			entity.Property(e => e.Comision)
					.HasColumnType("numeric(19, 4)")
					.HasColumnName("comision");

			entity.Property(e => e.DescripcionNegocio)
					.HasMaxLength(100)
					.IsUnicode(false);

			entity.Property(e => e.FechaApertura).HasColumnType(DATETIME);

			entity.Property(e => e.InicialNegocio)
					.HasMaxLength(4)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.Iva).HasColumnName("iva");

			entity.Property(e => e.NegocioActivo)
					.HasMaxLength(1)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.PrcTasaInteres).HasColumnName("prc_TasaInteres");

			entity.Property(e => e.Subcuenta).HasColumnName("subcuenta");
		});

		modelBuilder.Entity<Ctl_TasasIntere>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("ctl_TasasInteres");

			entity.Property(e => e.ClvAltoriesgo)
					.HasColumnName("clv_altoriesgo")
					.HasComment("Indica si es tasas para articulo de alto riesgo muebles");

			entity.Property(e => e.ClvCelulares)
					.HasColumnName("clv_celulares")
					.HasComment("Indica si es tasa para celulares muebles");

			entity.Property(e => e.ClvEspecial).HasColumnName("clv_especial");

			entity.Property(e => e.DesTasainteres)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("des_tasainteres")
					.HasDefaultValueSql("('')")
					.HasComment("Descripcion de la tasa de interes");

			entity.Property(e => e.IduCartera)
					.HasColumnName("idu_cartera")
					.HasComment("Identificador de la cartera");

			entity.Property(e => e.IduPlazo)
					.HasColumnName("idu_plazo")
					.HasComment("Contiene el identificador del plazo");

			entity.Property(e => e.IduTipotasa)
					.HasColumnName("idu_tipotasa")
					.HasComment("Indica si es normal o fronteriza.");

			entity.Property(e => e.PrcTasainteres)
					.HasColumnType("numeric(10, 2)")
					.HasColumnName("prc_tasainteres")
					.HasComment("Porcentaje de tasa de interes");
		});

		modelBuilder.Entity<CtlTasasIntere>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("ctlTasasInteres");

			entity.Property(e => e.Fecha).HasColumnType("smalldatetime");

			entity.Property(e => e.TasaPrestamosPlazo18).HasColumnType("numeric(18, 2)");

			entity.Property(e => e.TasaPrestamosPlazo24).HasColumnType("numeric(18, 2)");
		});

		modelBuilder.Entity<CatTasareestructura>(entity =>
		{
			entity.HasKey(e => new { e.IduProducto, e.NumPlazo });

			entity.ToTable("cat_tasareestructura");

			entity.Property(e => e.IduProducto)
					.HasColumnName("idu_Producto")
					.HasComment("Identificador del producto de reestructura de crédito al que pertenece el plazo y la tasa interés.");

			entity.Property(e => e.NumPlazo)
					.HasColumnName("num_plazo")
					.HasComment("Plazo en meses que se otorga al cliente de acuerdo al producto de reestructura al que aplica dicho cliente.");

			entity.Property(e => e.NumTasa)
					.HasColumnName("num_tasa")
					.HasComment("Porcentaje de la tasa interés que se aplicara al producto de reestructura a la que aplica el cliente, según el plazo .");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	public virtual DbSet<CatCiudade> CatCiudades { get; set; } = null!;
	public virtual DbSet<CatFactorescartera> CatFactorescarteras { get; set; } = null!;
	public virtual DbSet<CatFactorportipoproducto> CatFactorportipoproductos { get; set; } = null!;
	public virtual DbSet<CtlMaestraFecha> CtlMaestraFechas { get; set; } = null!;
	public virtual DbSet<CatNegocio> CatNegocios { get; set; } = null!;
	public virtual DbSet<Ctl_TasasIntere> Ctl_TasasInteres { get; set; } = null!;
	public virtual DbSet<CtlTasasIntere> CtlTasasInteres { get; set; } = null!;
	public virtual DbSet<CatTasareestructura> CatTasareestructuras { get; set; } = null!;
}
