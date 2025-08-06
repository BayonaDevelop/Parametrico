using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Core.Catalogos;
using Microsoft.EntityFrameworkCore;
using System;

namespace Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;

public partial class CatalogosDbContext : DbContext
{
	public CatalogosDbContext() {}

	public CatalogosDbContext(DbContextOptions<CatalogosDbContext> options) : base(options) { }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer(Utils.GetConnectionStrings().Find(i => StringComparer.OrdinalIgnoreCase.Equals(i.Key, Enum.GetName(typeof(DatabaseType), DatabaseType.Catalogos)!)).Value);
		}
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CatBasesDeDato>(entity =>
		{
			entity.HasKey(e => e.IdBd)
					.HasName("PK__catBases__B773A9017C14EF92");

			entity.ToTable("catBasesDeDatos");

			entity.Property(e => e.IdBd).ValueGeneratedNever();

			entity.Property(e => e.DescBd)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("descBd");
		});

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
					.HasColumnName("fechaAlta");

			entity.Property(e => e.PrcBonificacionNueva)
					.HasColumnType("numeric(10, 4)")
					.HasColumnName("prc_bonificacionNueva");
		});

		modelBuilder.Entity<HisCatBonificacionesPrestamo>(entity =>
		{
			entity
					.HasNoKey()
					.ToTable("his_catbonificacionesPrestamos");

			entity.Property(e => e.FecCorte)
					.HasColumnType("date")
					.HasColumnName("fec_corte");
			entity.Property(e => e.FecMovimiento)
					.HasColumnType("smalldatetime")
					.HasColumnName("fec_movimiento");
			entity.Property(e => e.FechaAlta)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("""datetime""")
					.HasColumnName("fechaAlta");
			entity.Property(e => e.NumDiasTranscurridos).HasColumnName("num_diasTranscurridos");
			entity.Property(e => e.NumPlazo).HasColumnName("num_plazo");
			entity.Property(e => e.PrcBonificacion)
					.HasColumnType("numeric(10, 4)")
					.HasColumnName("prc_bonificacion");
			entity.Property(e => e.PrcBonificacionNueva)
					.HasColumnType("numeric(10, 4)")
					.HasColumnName("prc_bonificacionNueva");

			entity.HasOne(d => d.BaseDatosOrigenNavigation).WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_his_catbonificacionesPrestamos");
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
					.HasColumnName("fechaAlta");
		});

		modelBuilder.Entity<HisCatBonificacionescartera>(entity =>
		{
			entity
					.HasNoKey()
					.ToTable("his_catbonificacionescarteras");

			entity.Property(e => e.FechaAlta)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta");
			entity.Property(e => e.IduCartera).HasColumnName("idu_cartera");
			entity.Property(e => e.IduPlazo).HasColumnName("idu_plazo");
			entity.Property(e => e.NumDiastranscurridos).HasColumnName("num_diastranscurridos");
			entity.Property(e => e.NumMesestranscurridos).HasColumnName("num_mesestranscurridos");
			entity.Property(e => e.PrcBonificacion)
					.HasColumnType("decimal(10, 4)")
					.HasColumnName("prc_bonificacion");

			entity.HasOne(d => d.BaseDatosOrigenNavigation).WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_his_catbonificacionescarteras");
		});

		modelBuilder.Entity<CatCiudade>(entity =>
		{
			entity.HasNoKey();

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
		});

		modelBuilder.Entity<CatCiudadesHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("CatCiudades_Historial");

			entity.Property(e => e.AntiguedadCiudad).HasColumnType("datetime");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

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

			entity.HasOne(d => d.BaseDatosOrigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_CatCiudades_Historial");
		});

		modelBuilder.Entity<CatCorreosOperacione>(entity =>
		{
			entity.HasKey(e => e.IdCorreo)
					.HasName("PK__catCorre__D247A9095054FC8F");

			entity.ToTable("catCorreosOperaciones");

			entity.Property(e => e.IdCorreo).HasColumnName("idCorreo");

			entity.Property(e => e.Correo)
					.HasMaxLength(80)
					.IsUnicode(false)
					.HasColumnName("correo");

			entity.Property(e => e.FeAltaMail)
					.HasColumnType("datetime")
					.HasColumnName("feAltaMail")
					.HasDefaultValueSql("getdate()");

			entity.Property(e => e.NomPersonaCorreo)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("nomPersonaCorreo");

			entity.Property(e => e.Tipo).HasColumnName("tipo");
		});

		modelBuilder.Entity<CatCrbonificacionesHistorialCl>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_crbonificaciones_HistorialCL");

			entity.Property(e => e.BaseDatosOrigen)
					.HasColumnName("baseDatosOrigen")
					.HasDefaultValueSql("((1))");

			entity.Property(e => e.FecMov).HasColumnName("fec_mov");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.IduCuenta).HasColumnName("idu_cuenta");

			entity.Property(e => e.NumFactorajuste).HasColumnName("num_factorajuste");

			entity.Property(e => e.NumPlazo).HasColumnName("num_plazo");

			entity.Property(e => e.NumPorcentaje).HasColumnName("num_porcentaje");

			entity.Property(e => e.NumTiempotranscurrido).HasColumnName("num_tiempotranscurrido");

			entity.Property(e => e.OpcBonificacion)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("opc_bonificacion")
					.HasDefaultValueSql("('')");

			entity.Property(e => e.OpcTiempotranscurrido)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("opc_tiempotranscurrido")
					.HasDefaultValueSql("('')");

			entity.Property(e => e.SecKeyx).HasColumnName("sec_keyx");
		});

		modelBuilder.Entity<CatCrconfiguracionesplazosAdp>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_crconfiguracionesplazos_ADP");

			entity.Property(e => e.CtrlActualizacion).HasColumnName("ctrl_Actualizacion");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fecha_Alta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.IduCuenta).HasColumnName("idu_cuenta");

			entity.Property(e => e.NumPlazo).HasColumnName("num_plazo");

			entity.Property(e => e.PrcInteressobrecompra).HasColumnName("prc_interessobrecompra");
		});

		modelBuilder.Entity<CatCrconfiguracionesplazosHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_crconfiguracionesplazos_Historial");

			entity.Property(e => e.BaseDatosOrigen)
					.HasColumnName("baseDatosOrigen")
					.HasDefaultValueSql("((7))");

			entity.Property(e => e.CtlProceso).HasColumnName("ctlProceso");

			entity.Property(e => e.FecMovto).HasColumnName("fec_movto");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fecha_Alta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.IduCuenta).HasColumnName("idu_cuenta");

			entity.Property(e => e.NumFactorlineacreditobasica).HasColumnName("num_factorlineacreditobasica");

			entity.Property(e => e.NumFactorlineacreditoespecial).HasColumnName("num_factorlineacreditoespecial");

			entity.Property(e => e.NumFactorlineacreditominima).HasColumnName("num_factorlineacreditominima");

			entity.Property(e => e.NumFactorlineacreditonormal).HasColumnName("num_factorlineacreditonormal");

			entity.Property(e => e.NumPlazo).HasColumnName("num_plazo");

			entity.Property(e => e.PrcInteressobrecompra).HasColumnName("prc_interessobrecompra");
		});

		modelBuilder.Entity<CatCrinteresmoratorioAdp>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_crinteresmoratorio_ADP");

			entity.Property(e => e.CtrlActualizacion).HasColumnName("ctrl_Actualizacion");

			entity.Property(e => e.FecMovto).HasColumnName("fec_movto");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fecha_Alta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.IduCiudad)
					.HasColumnName("idu_ciudad")
					.HasDefaultValueSql("((0))");

			entity.Property(e => e.IduCuenta)
					.HasColumnName("idu_cuenta")
					.HasDefaultValueSql("((0))");

			entity.Property(e => e.NumPorcentajeint)
					.HasColumnName("num_porcentajeint")
					.HasDefaultValueSql("((0))");
		});

		modelBuilder.Entity<CatCrinteresmoratorioHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_crinteresmoratorio_Historial");

			entity.Property(e => e.FechaAlta)
				.HasColumnName("fecha_Alta")
				.HasColumnType("datetime");

			entity.Property(e => e.BaseDatosOrigen)
				.HasColumnName("baseDatosOrigen");

			entity.Property(e => e.CtlProceso)
				.HasColumnName("ctlProceso");

			entity.Property(e => e.IduCiudad)
				.HasColumnName("idu_ciudad");

			entity.Property(e => e.IduCiudad)
				.HasColumnName("idu_cuenta");

			entity.Property(e => e.NumPorcentajeint)
				.HasColumnName("num_porcentajeint");

			entity.Property(e => e.FecMovto)
				.HasColumnName("fec_movto")
				.HasColumnType("datetime");
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

		modelBuilder.Entity<CatFactorescarterasHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_factorescarteras_Historial");

			entity.Property(e => e.BaseDatosOrigen).HasColumnName("baseDatosOrigen");

			entity.Property(e => e.ClvLineacreditoespecial).HasColumnName("clv_lineacreditoespecial");

			entity.Property(e => e.ClvPlazo).HasColumnName("clv_plazo");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.IduCartera).HasColumnName("idu_cartera");

			entity.Property(e => e.PrcFactor)
					.HasColumnType("numeric(18, 2)")
					.HasColumnName("prc_factor");

			entity.HasOne(d => d.BaseDatosOrigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_cat_factorescarteras_Historial");
		});

		modelBuilder.Entity<CatFactorescarterasTmp>(entity =>
		{
			entity.ToTable("cat_factorescarteras_tmp");

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

		modelBuilder.Entity<CatFactorportipoproductoHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_factorportipoproducto_Historial");

			entity.Property(e => e.BaseDatosOrigen).HasColumnName("baseDatosOrigen");

			entity.Property(e => e.DesTipoproducto)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("des_tipoproducto");

			entity.Property(e => e.FecMovto)
					.HasColumnType("datetime")
					.HasColumnName("fec_movto");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.IduControl).HasColumnName("idu_control");

			entity.Property(e => e.NumFactor).HasColumnName("num_factor");

			entity.Property(e => e.NumPlazo).HasColumnName("num_plazo");

			entity.Property(e => e.NumTipolineacredito).HasColumnName("num_tipolineacredito");

			entity.Property(e => e.NumTipoproducto).HasColumnName("num_tipoproducto");

			entity.HasOne(d => d.BaseDatosOrigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_cat_factorportipoproducto");
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

		modelBuilder.Entity<CatInteresesMoratoriosHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("CatInteresesMoratorios_Historial");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta");

			entity.Property(e => e.PorcentajeInteresMoratorio)
					.HasMaxLength(8)
					.IsUnicode(false)
					.IsFixedLength();

			entity.HasOne(d => d.BaseDatosOrigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_CatInteresesMoratorios_Historial");
		});

		modelBuilder.Entity<CatLimCredClientesNuevo>(entity =>
		{
			entity.HasNoKey();
		});

		modelBuilder.Entity<CatLimCredClientesNuevosHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("CatLimCredClientesNuevos_Historial");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta");

			entity.HasOne(d => d.BaseDatosOrigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_CatLimCredClientesNuevos_Historial");
		});

		modelBuilder.Entity<CatLogicaAsignacionQuebrantadaHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_LogicaAsignacionQuebrantada_Historial");

			entity.Property(e => e.BaseDatosOrigen).HasColumnName("baseDatosOrigen");

			entity.Property(e => e.ClvDespacho).HasColumnName("clv_despacho");

			entity.Property(e => e.ClvPuntualidad)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("clv_puntualidad")
					.HasDefaultValueSql("('')")
					.IsFixedLength();

			entity.Property(e => e.ClvPuntualidadactiva)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("clv_puntualidadactiva")
					.HasDefaultValueSql("('')")
					.IsFixedLength();

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.FlagVentaEspecial).HasColumnName("flag_ventaEspecial");

			entity.Property(e => e.NumAbonovencidosFin).HasColumnName("num_abonovencidosFin");

			entity.Property(e => e.NumAbonovencidosInicio).HasColumnName("num_abonovencidosInicio");

			entity.Property(e => e.NumPlazo).HasColumnName("num_plazo");

			entity.Property(e => e.NumTasa).HasColumnName("num_tasa");

			entity.Property(e => e.PrcInteres)
					.HasColumnType("decimal(6, 4)")
					.HasColumnName("prc_interes");

			entity.HasOne(d => d.BaseDatosOrigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_cat_LogicaAsignacionQuebrantada_Historial");
		});

		modelBuilder.Entity<CatLogicaAsignacionQuebrantadum>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_LogicaAsignacionQuebrantada");

			entity.Property(e => e.ClvDespacho).HasColumnName("clv_despacho");

			entity.Property(e => e.ClvPuntualidad)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("clv_puntualidad")
					.HasDefaultValueSql("('')")
					.IsFixedLength();

			entity.Property(e => e.ClvPuntualidadactiva)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("clv_puntualidadactiva")
					.HasDefaultValueSql("('')")
					.IsFixedLength();

			entity.Property(e => e.FlagVentaEspecial).HasColumnName("flag_ventaEspecial");

			entity.Property(e => e.NumAbonovencidosFin).HasColumnName("num_abonovencidosFin");

			entity.Property(e => e.NumAbonovencidosInicio).HasColumnName("num_abonovencidosInicio");

			entity.Property(e => e.NumPlazo).HasColumnName("num_plazo");

			entity.Property(e => e.NumTasa).HasColumnName("num_tasa");

			entity.Property(e => e.PrcInteres)
					.HasColumnType("decimal(6, 4)")
					.HasColumnName("prc_interes");
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

			entity.Property(e => e.FechaApertura).HasColumnType("datetime");

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

		modelBuilder.Entity<CatNegociosHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("CatNegocios_Historial");

			entity.Property(e => e.BaseDatosOrigen).HasColumnName("baseDatosOrigen");

			entity.Property(e => e.Comision)
					.HasColumnType("numeric(19, 4)")
					.HasColumnName("comision");

			entity.Property(e => e.DescripcionNegocio)
					.HasMaxLength(100)
					.IsUnicode(false);

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.FechaApertura).HasColumnType("datetime");

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

			entity.HasOne(d => d.BaseDatosOrigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_CatNegocios_Historial");
		});

		modelBuilder.Entity<CatSalariosMinimo>(entity =>
		{
			entity.HasNoKey();

			entity.Property(e => e.FechaSalario).HasColumnType("datetime");
		});

		modelBuilder.Entity<CatSalariosMinimosHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("CatSalariosMinimos_Historial");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta");

			entity.Property(e => e.FechaSalario).HasColumnType("datetime");

			entity.HasOne(d => d.BaseDatosOrigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_CatSalariosMinimos_Historial");
		});

		modelBuilder.Entity<CatSalariosminimosctasperdida>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_salariosminimosctasperdidas");

			entity.Property(e => e.FecSalario)
					.HasColumnType("datetime")
					.HasColumnName("FEC_SALARIO");

			entity.Property(e => e.IdSalario).HasColumnName("ID_SALARIO");

			entity.Property(e => e.ImpSalarioctasperdidas).HasColumnName("IMP_SALARIOCTASPERDIDAS");
		});

		modelBuilder.Entity<CatSalariosminimosctasperdidasHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_salariosminimosctasperdidas_Historial");

			entity.Property(e => e.Basedatosorigen).HasColumnName("baseDatosOrigen".ToUpper());

			entity.Property(e => e.FecSalario)
					.HasColumnType("datetime")
					.HasColumnName("FEC_SALARIO");

			entity.Property(e => e.Fechaalta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta".ToUpper())
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.IdSalario).HasColumnName("ID_SALARIO");

			entity.Property(e => e.ImpSalarioctasperdidas).HasColumnName("IMP_SALARIOCTASPERDIDAS");

			entity.HasOne(d => d.BasedatosorigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.Basedatosorigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_cat_salariosminimosctasperdidas_Historial");
		});

		modelBuilder.Entity<CatTasareestructura>(entity =>
		{
			entity.HasKey(e => new { e.IduProducto, e.NumPlazo })
					.HasName("PK__cat_tasa__3B50757D9F8EC8EB");

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

		modelBuilder.Entity<CatTasareestructuraHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_tasareestructura_Historial");

			entity.Property(e => e.BaseDatosOrigen).HasColumnName("baseDatosOrigen");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.IduProducto).HasColumnName("idu_Producto");

			entity.Property(e => e.NumPlazo).HasColumnName("num_plazo");

			entity.Property(e => e.NumTasa).HasColumnName("num_tasa");

			entity.HasOne(d => d.BaseDatosOrigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_cat_tasareestructura_Historial");
		});

		modelBuilder.Entity<CatTiposlineacredito>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_tiposlineacredito");

			entity.Property(e => e.DesTipolineacredito)
					.HasMaxLength(10)
					.IsUnicode(false)
					.HasColumnName("des_tipolineacredito");

			entity.Property(e => e.FecRegistro)
					.HasColumnType("datetime")
					.HasColumnName("fec_registro");

			entity.Property(e => e.ImpMaximolineacreditoreal).HasColumnName("imp_maximolineacreditoreal");

			entity.Property(e => e.ImpMinimolineacreditoreal).HasColumnName("imp_minimolineacreditoreal");

			entity.Property(e => e.NumTipolineacredito).HasColumnName("num_tipolineacredito");
		});

		modelBuilder.Entity<CtlBonificacionCartera>(entity =>
		{
			entity.HasKey(e => e.IdBonificacion)
					.HasName("PK__ctl_boni__780E855FBEC6FDA5");

			entity.ToTable("ctl_bonificacionCarteras");

			entity.Property(e => e.IdBonificacion).HasColumnName("idBonificacion");

			entity.Property(e => e.Estatus).HasColumnName("estatus");

			entity.Property(e => e.EstatusCl).HasColumnName("estatusCL");

			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");

			entity.Property(e => e.FechaActualizacionCl)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacionCL");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");

			entity.Property(e => e.IdPlazo).HasColumnName("idPlazo");

			entity.Property(e => e.NumDiastranscurridos).HasColumnName("num_diastranscurridos");

			entity.Property(e => e.NumPorcentajebonificacion)
					.HasColumnType("decimal(10, 4)")
					.HasColumnName("num_porcentajebonificacion");

			entity.HasOne(d => d.EstatusNavigation)
					.WithMany(p => p.CtlBonificacionCarteraEstatusNavigations)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_bonificacionCarteras");

			entity.HasOne(d => d.EstatusClNavigation)
					.WithMany(p => p.CtlBonificacionCarteraEstatusClNavigations)
					.HasForeignKey(d => d.EstatusCl)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_bonificacionCarteras_CarteraEnLinea");

			entity.HasOne(d => d.IdPlazoNavigation)
					.WithMany(p => p.CtlBonificacionCarteras)
					.HasForeignKey(d => d.IdPlazo)
					.HasConstraintName("fk_ctl_bonificacionCarteras_Plazos");
		});

		modelBuilder.Entity<CtlCatalogoPlazo>(entity =>
		{
			entity.HasKey(e => e.IdPlazo);

			entity.ToTable("ctl_Catalogo_Plazos");

			entity.Property(e => e.IdPlazo).HasColumnName("idPlazo");

			entity.Property(e => e.Cartera)
					.HasMaxLength(50)
					.HasColumnName("cartera");

			entity.Property(e => e.IduCartera).HasColumnName("idu_cartera");

			entity.Property(e => e.IduCuenta).HasColumnName("idu_cuenta");

			entity.Property(e => e.Plazo).HasColumnName("plazo");

			entity.Property(e => e.TipoTransaccion)
					.HasMaxLength(50)
					.HasColumnName("tipoTransaccion");
		});

		modelBuilder.Entity<CtlConversionLineadeCredito>(entity =>
		{
			entity.HasKey(e => e.IdConversion)
					.HasName("PK__ctl_Conv__BDB7F9026AFEF61C");

			entity.ToTable("ctl_ConversionLineadeCredito");

			entity.Property(e => e.IdConversion).HasColumnName("idConversion");

			entity.Property(e => e.EdadMaxPerfil).HasColumnName("edadMaxPerfil");

			entity.Property(e => e.EdadMinPerfil).HasColumnName("edadMinPerfil");

			entity.Property(e => e.Estatus).HasColumnName("estatus");

			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.FechaVigenteDesde)
					.HasColumnType("datetime")
					.HasColumnName("fechaVigenteDesde");

			entity.Property(e => e.ImporteVencido)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("importeVencido");

			entity.Property(e => e.NomGrupo)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("nomGrupo");

			entity.Property(e => e.NumMesMinDesdePrimerCompra).HasColumnName("numMesMinDesdePrimerCompra");

			entity.Property(e => e.NumMesesCalcularLcr).HasColumnName("numMesesCalcularLCR");

			entity.Property(e => e.PorcentajePerfil)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("porcentajePerfil");

			entity.Property(e => e.PorcentajecCalculoCsa)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("porcentajecCalculoCSA");

			entity.Property(e => e.Puntualidad)
					.HasMaxLength(10)
					.IsUnicode(false)
					.HasColumnName("puntualidad");

			entity.Property(e => e.TopeEdadMax).HasColumnName("topeEdadMax");

			entity.Property(e => e.TopeMaxPerfil)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("topeMaxPerfil");

			entity.Property(e => e.TopeMinPerfil)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("topeMinPerfil");

			entity.HasOne(d => d.EstatusNavigation)
					.WithMany(p => p.CtlConversionLineadeCreditos)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ConversionLineadeCredito");
		});

		modelBuilder.Entity<CtlDecrementosLineadeCredito>(entity =>
		{
			entity.HasKey(e => e.IdDecremento)
					.HasName("PK__ctl_Decr__10DA0DACC9D66CDF");

			entity.ToTable("ctl_DecrementosLineadeCredito");

			entity.Property(e => e.IdDecremento).HasColumnName("idDecremento");

			entity.Property(e => e.Estatus).HasColumnName("estatus");

			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.FechaArranque)
					.HasColumnName("fechaArranque")
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");

			entity.Property(e => e.NomPuntualidad)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("nomPuntualidad");

			entity.Property(e => e.NomTipoLinea)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("nomTipoLinea");

			entity.Property(e => e.NumDecremento)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("numDecremento");

			entity.Property(e => e.NumEficienciaHis)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("numEficienciaHis");

			entity.Property(e => e.NumTipoLinea).HasColumnName("numTipoLinea");

			entity.Property(e => e.NumTopeLineaMinima)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("numTopeLineaMinima");

			entity.HasOne(d => d.EstatusNavigation)
					.WithMany(p => p.CtlDecrementosLineadeCreditos)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_DecrementosLineadeCredito");
		});

		modelBuilder.Entity<CtlEstatusParametrico>(entity =>
		{
			entity.HasKey(e => e.IdEstatus)
										.HasName("PK__ctl_Esta__DCBE18B6A911C005");

			entity.ToTable("ctl_EstatusParametrico");

			entity.Property(e => e.IdEstatus)
					.ValueGeneratedNever()
					.HasColumnName("idEstatus");

			entity.Property(e => e.DescripionEstatus)
					.HasMaxLength(30)
					.IsUnicode(false)
					.HasColumnName("descripionEstatus");
		});

		modelBuilder.Entity<CtlFactoresSaturacionCartera>(entity =>
		{
			entity.HasKey(e => e.IdFacSaturacion)
					.HasName("PK__ctl_Fact__A2A48B08AF82F22A");

			entity.ToTable("ctl_FactoresSaturacionCartera");

			entity.Property(e => e.IdFacSaturacion).HasColumnName("idFacSaturacion");

			entity.Property(e => e.Cartera)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("cartera");

			entity.Property(e => e.Estatus).HasColumnName("estatus");

			entity.Property(e => e.FactorEspecial)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("factorEspecial");

			entity.Property(e => e.FactorInicial)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("factorInicial");

			entity.Property(e => e.FactorMinima)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("factorMinima");

			entity.Property(e => e.FactorNormal)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("factorNormal");

			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");

			entity.Property(e => e.Plazo).HasColumnName("plazo");

			entity.HasOne(d => d.EstatusNavigation)
					.WithMany(p => p.CtlFactoresSaturacionCarteras)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_FactoresSaturacionCartera");
		});

		modelBuilder.Entity<CtlInteresMoratorioDcp>(entity =>
		{
			entity.HasKey(e => e.IdInteresDcp)
					.HasName("PK__ctl_Inte__A9A469790EB577B5");

			entity.ToTable("ctl_InteresMoratorioDCP");

			entity.Property(e => e.IdInteresDcp).HasColumnName("idInteresDCP");

			entity.Property(e => e.DiasTranscurridos).HasColumnName("diasTranscurridos");

			entity.Property(e => e.Estatus).HasColumnName("estatus");

			entity.Property(e => e.EstatusCl).HasColumnName("estatusCL");

			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");

			entity.Property(e => e.FechaActualizacionCl)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacionCL");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.Fechains)
					.HasColumnType("datetime")
					.HasColumnName("fechains");

			entity.Property(e => e.TasaMoratoria)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("tasaMoratoria");

			entity.Property(e => e.Usermodifico)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("usermodifico");

			entity.HasOne(d => d.EstatusNavigation)
					.WithMany(p => p.CtlInteresMoratorioDcps)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_InteresMoratorioDCP");
		});

    modelBuilder.Entity<CtlInteresMoratorioporCartera>(entity =>
    {
      entity.HasKey(e => e.IdIntMoratorio).HasName("PK__ctl_Inte__638C6748ABFF63B9");

      entity.ToTable("ctl_InteresMoratorioporCartera");

      entity.Property(e => e.IdIntMoratorio).HasColumnName("idIntMoratorio");
      entity.Property(e => e.Estatus).HasColumnName("estatus");
      entity.Property(e => e.EstatusCl).HasColumnName("estatusCL");
      entity.Property(e => e.FechaActualizacion)
          .HasColumnType("datetime")
          .HasColumnName("fechaActualizacion");
      entity.Property(e => e.FechaActualizacionCl)
          .HasColumnType("datetime")
          .HasColumnName("fechaActualizacionCL");
      entity.Property(e => e.FechaAlta)
          .HasDefaultValueSql("(getdate())")
          .HasColumnType("datetime")
          .HasColumnName("fechaAlta");
      entity.Property(e => e.FechaArranque)
          .HasColumnType("datetime")
          .HasColumnName("fechaArranque");
      entity.Property(e => e.TasaTipoCiudad1)
          .HasColumnType("decimal(8, 2)")
          .HasColumnName("tasaTipoCiudad1");
      entity.Property(e => e.TasaTipoCiudad2)
          .HasColumnType("decimal(8, 2)")
          .HasColumnName("tasaTipoCiudad2");

      entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.CtlInteresMoratorioporCarteraEstatusNavigations)
          .HasForeignKey(d => d.Estatus)
          .OnDelete(DeleteBehavior.ClientSetNull)
          .HasConstraintName("fk_ctl_InteresMoratorioporCartera");

      entity.HasOne(d => d.EstatusClNavigation).WithMany(p => p.CtlInteresMoratorioporCarteraEstatusClNavigations)
          .HasForeignKey(d => d.EstatusCl)
          .OnDelete(DeleteBehavior.ClientSetNull)
          .HasConstraintName("fk_ctl_InteresMoratorioporCarteraEnLinea");
    });

    modelBuilder.Entity<CtlParametrosautenticacion>(entity =>
		{
			entity.HasKey(e => e.IdParametro)
					.HasName("PK__ctl_para__EFB0B7E47F4CFCCF");

			entity.ToTable("ctl_parametrosautenticacion");

			entity.Property(e => e.IdParametro).HasColumnName("id_Parametro");
			entity.Property(e => e.Idc).HasColumnName("IDC");
			entity.Property(e => e.NombreParametro)
					.HasMaxLength(100)
					.IsUnicode(false)
					.HasColumnName("nombre_Parametro");
			entity.Property(e => e.Tipo)
					.HasMaxLength(20)
					.IsUnicode(false)
					.HasColumnName("tipo");
			entity.Property(e => e.ValorParametro)
					.HasColumnType("text")
					.HasColumnName("valor_Parametro");
		});

		modelBuilder.Entity<CtlPuntosdeconsumo>(entity =>
		{
			entity.HasKey(e => e.IdFuncionalidad).HasName("pk_CtlPuntosDeConsumo");

			entity.ToTable("ctl_puntosdeconsumo");

			entity.Property(e => e.IdFuncionalidad)
					.ValueGeneratedNever()
					.HasColumnName("id_Funcionalidad");
			entity.Property(e => e.AllowAfter20).HasColumnName("Allow_After_20");
			entity.Property(e => e.NomFuncionalidad).HasColumnName("nom_Funcionalidad");
			entity.Property(e => e.NomTbDestino).HasColumnName("nom_TbDestino");
			entity.Property(e => e.RutaServicio).HasColumnName("ruta_Servicio");
		});

		modelBuilder.Entity<CtlSalarioMinimoCp>(entity =>
		{
			entity.HasKey(e => e.IdSalario)
										.HasName("PK__ctl_Sala__A58003576800F490");

			entity.ToTable("ctl_SalarioMinimoCP");

			entity.Property(e => e.IdSalario).HasColumnName("idSalario");

			entity.Property(e => e.Estatus).HasColumnName("estatus");

			entity.Property(e => e.EstatusCl).HasColumnName("estatusCL");

			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");

			entity.Property(e => e.FechaActualizacionCl)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacionCL");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");

			entity.Property(e => e.NombreSalario)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("nombreSalario");

			entity.Property(e => e.ValorSalario)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("valorSalario");

			entity.HasOne(d => d.EstatusNavigation)
					.WithMany(p => p.CtlSalarioMinimoCpEstatusNavigations)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_EstatusMinimoCP");

			entity.HasOne(d => d.EstatusClNavigation)
					.WithMany(p => p.CtlSalarioMinimoCpEstatusClNavigations)
					.HasForeignKey(d => d.EstatusCl)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_EstatusMinimoCP_CarteraEnLinea");
		});

		modelBuilder.Entity<CtlSalarioMinimoLc>(entity =>
		{
			entity.HasKey(e => e.IdSalario)
					.HasName("PK__ctl_Sala__A5800357DC727CE6");

			entity.ToTable("ctl_SalarioMinimoLC");

			entity.Property(e => e.IdSalario).HasColumnName("idSalario");

			entity.Property(e => e.Estatus).HasColumnName("estatus");

			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");

			entity.Property(e => e.NombreSalario)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("nombreSalario");

			entity.Property(e => e.ValorSalario)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("valorSalario");

			entity.HasOne(d => d.EstatusNavigation)
					.WithMany(p => p.CtlSalarioMinimoLcs)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_EstatusParametrico");
		});

		modelBuilder.Entity<CtlTasaInteresCpprestamo>(entity =>
		{
			entity.HasKey(e => e.IdTasasP)
					.HasName("PK__ctl_Tasa__9DF813D37A3485ED");

			entity.ToTable("ctl_TasaInteresCPPrestamo");

			entity.Property(e => e.IdTasasP).HasColumnName("idTasasP");

			entity.Property(e => e.Cartera)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("cartera");

			entity.Property(e => e.Estatus).HasColumnName("estatus");

			entity.Property(e => e.EstatusCl)
					.HasColumnName("estatusCL")
					.HasDefaultValueSql("((1))");

			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");

			entity.Property(e => e.FechaActualizacionCl)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacionCL");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");

			entity.Property(e => e.Grupo)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("grupo");

			entity.Property(e => e.Plazo).HasColumnName("plazo");

			entity.Property(e => e.PuntajeFinal).HasColumnName("puntajeFinal");

			entity.Property(e => e.PuntajeInicial).HasColumnName("puntajeInicial");

			entity.Property(e => e.Puntualidad)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("puntualidad");

			entity.Property(e => e.TasaDeInteres)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("tasaDeInteres");

			entity.HasOne(d => d.EstatusNavigation)
					.WithMany(p => p.CtlTasaInteresCpprestamoEstatusNavigations)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_TasaInteresCPPrestamo");

			entity.HasOne(d => d.EstatusClNavigation)
					.WithMany(p => p.CtlTasaInteresCpprestamoEstatusClNavigations)
					.HasForeignKey(d => d.EstatusCl)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_estatuscl");
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

		modelBuilder.Entity<CtlTasasInteresCp>(entity =>
		{
			entity.HasKey(e => e.IdTasas)
					.HasName("PK__ctl_Tasa__775C29036EC2D341");

			entity.ToTable("ctl_TasasInteresCP");

			entity.Property(e => e.IdTasas).HasColumnName("idTasas");

			entity.Property(e => e.Cartera)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("cartera");

			entity.Property(e => e.Estatus).HasColumnName("estatus");

			entity.Property(e => e.EstatusCl)
					.HasColumnName("estatusCL")
					.HasDefaultValueSql("((1))");

			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");

			entity.Property(e => e.FechaActualizacionCl)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacionCL");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");

			entity.Property(e => e.Plazo).HasColumnName("plazo");

			entity.Property(e => e.TasaDeInteres)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("tasaDeInteres");

			entity.HasOne(d => d.EstatusNavigation)
					.WithMany(p => p.CtlTasasInteresCps)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_TasasInteresCP");
		});

		modelBuilder.Entity<CtlTasasInteresCpc>(entity =>
		{
			entity.HasKey(e => e.IdTasasCpc)
					.HasName("PK__ctl_Tasa__9AA714AB5FD5956C");

			entity.ToTable("ctl_TasasInteresCPC");

			entity.Property(e => e.IdTasasCpc).HasColumnName("idTasasCPC");

			entity.Property(e => e.Articulo)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("articulo");

			entity.Property(e => e.Cartera)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("cartera");

			entity.Property(e => e.Ciudad).HasColumnName("ciudad");

			entity.Property(e => e.Estatus).HasColumnName("estatus");

			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");

			entity.Property(e => e.Plazo).HasColumnName("plazo");

			entity.Property(e => e.TasaDeInteres)
					.HasColumnType("decimal(8, 2)")
					.HasColumnName("tasaDeInteres");

			entity.HasOne(d => d.EstatusNavigation)
					.WithMany(p => p.CtlTasasInteresCpcs)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_TasasInteresCPC");
		});

		modelBuilder.Entity<Ctl_TasasInteresHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("ctl_TasasInteres_Historial");

			entity.Property(e => e.BaseDatosOrigen)
					.HasColumnName("baseDatosOrigen")
					.HasDefaultValueSql("((1))");

			entity.Property(e => e.ClvAltoriesgo).HasColumnName("clv_altoriesgo");

			entity.Property(e => e.ClvCelulares).HasColumnName("clv_celulares");

			entity.Property(e => e.ClvEspecial).HasColumnName("clv_especial");

			entity.Property(e => e.DesTasainteres)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("des_tasainteres");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.IduCartera).HasColumnName("idu_cartera");

			entity.Property(e => e.IduPlazo).HasColumnName("idu_plazo");

			entity.Property(e => e.IduTipotasa).HasColumnName("idu_tipotasa");

			entity.Property(e => e.PrcTasainteres)
					.HasColumnType("numeric(10, 2)")
					.HasColumnName("prc_tasainteres");

			entity.HasOne(d => d.BaseDatosOrigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_TasasInteres_Historial");
		});

		modelBuilder.Entity<CtlTasasInteresHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("ctlTasasInteres_Historial");

			entity.Property(e => e.BaseDatosOrigen).HasColumnName("baseDatosOrigen");

			entity.Property(e => e.Fecha).HasColumnType("smalldatetime");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.TasaPrestamosPlazo18).HasColumnType("numeric(18, 2)");

			entity.Property(e => e.TasaPrestamosPlazo24).HasColumnType("numeric(18, 2)");
		});

		modelBuilder.Entity<ValidacionesLrc>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("ValidacionesLRC");

			entity.Property(e => e.ImpVencido).HasColumnName("imp_vencido");

			entity.Property(e => e.ImpVencidolrcmin).HasColumnName("imp_vencidolrcmin");

			entity.Property(e => e.NumCapacidadAbono)
					.HasColumnName("num_CapacidadAbono")
					.HasDefaultValueSql("((70))");

			entity.Property(e => e.NumEdadMaxima)
					.HasColumnName("num_EdadMaxima")
					.HasDefaultValueSql("((75))");

			entity.Property(e => e.NumEdadlrcmin).HasColumnName("num_edadlrcmin");

			entity.Property(e => e.NumFactorlinea).HasColumnName("num_factorlinea");

			entity.Property(e => e.NumMesesConSaldo)
					.HasColumnName("num_MesesConSaldo")
					.HasDefaultValueSql("((4))");

			entity.Property(e => e.NumMesescongelados).HasColumnName("num_mesescongelados");

			entity.Property(e => e.NumMesesconsaldolrcmin).HasColumnName("num_mesesconsaldolrcmin");

			entity.Property(e => e.NumMesescra).HasColumnName("num_mesescra");

			entity.Property(e => e.NumMesesprimercompra).HasColumnName("num_mesesprimercompra");

			entity.Property(e => e.NumMesesprimercompralrcmin).HasColumnName("num_mesesprimercompralrcmin");

			entity.Property(e => e.NumPuntosParPrestamos)
					.HasColumnName("num_PuntosParPrestamos")
					.HasDefaultValueSql("((51))");

			entity.Property(e => e.NumPuntosparcn).HasColumnName("num_puntosparcn");

			entity.Property(e => e.OpcPuntualidad)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("opc_puntualidad")
					.HasDefaultValueSql("('')")
					.IsFixedLength();

			entity.Property(e => e.PrcIngresomensual).HasColumnName("prc_ingresomensual");

			entity.Property(e => e.PrcPerfilgrupo1).HasColumnName("prc_perfilgrupo1");

			entity.Property(e => e.PrcPerfilgrupo2).HasColumnName("prc_perfilgrupo2");

			entity.Property(e => e.SalariosMinimosDism).HasColumnName("salariosMinimosDism");

			entity.Property(e => e.SituacionAumento)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("situacionAumento");

			entity.Property(e => e.Situaciondisminucion)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("situaciondisminucion");

			entity.Property(e => e.SituaciondisminucionCteGol)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("situaciondisminucionCteGol");
		});

		modelBuilder.Entity<ValidacionesLrcHistorial>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("ValidacionesLRC_Historial");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta")
					.HasDefaultValueSql("(getdate())");

			entity.Property(e => e.BaseDatosOrigen).HasColumnName("BaseDatosOrigen");

			entity.Property(e => e.SalariosMinimos)
				.HasColumnName("SalariosMinimos"); 
			
			entity.Property(e => e.SalariosMaximos)
				.HasColumnName("SalariosMaximos");

			entity.Property(e => e.MesesParaAumento)
				.HasColumnName("MesesParaAumento");

			entity.Property(e => e.MesesParaDisminucion)
				.HasColumnName("MesesParaDisminucion");

			entity.Property(e => e.PorcentajeCompra)
				.HasColumnName("PorcentajeCompra");

			entity.Property(e => e.FactorLineaTopeDeCredito)
				.HasColumnName("FactorLineaTopeDeCredito");

			entity.Property(e => e.EficienciaParaAumento)
				.HasColumnName("EficienciaParaAumento");

			entity.Property(e => e.EficienciaParaDisminucion)
				.HasColumnName("EficienciaParaDisminucion");

			entity.Property(e => e.PorcentajeAumento)
				.HasColumnName("PorcentajeAumento");

			entity.Property(e => e.PorcentajeDisminucion)
				.HasColumnName("PorcentajeDisminucion");

			entity.Property(e => e.SituacionAumento)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("situacionAumento");

			entity.Property(e => e.Situaciondisminucion)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("situaciondisminucion");

			entity.Property(e => e.SituaciondisminucionCteGol)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("situaciondisminucionCteGol");

			entity.Property(e => e.SalariosMinimosDism).HasColumnName("salariosMinimosDism");

			entity.Property(e => e.NumEdadMaxima)
					.HasColumnName("num_EdadMaxima")
					.HasDefaultValueSql("((75))");

			entity.Property(e => e.NumPuntosParPrestamos)
					.HasColumnName("num_PuntosParPrestamos")
					.HasDefaultValueSql("((51))");

			entity.Property(e => e.NumCapacidadAbono)
					.HasColumnName("num_CapacidadAbono")
					.HasDefaultValueSql("((70))");

			entity.Property(e => e.NumMesesConSaldo)
					.HasColumnName("num_MesesConSaldo")
					.HasDefaultValueSql("((4))");

			entity.Property(e => e.NumMesesprimercompra).HasColumnName("num_mesesprimercompra");

			entity.Property(e => e.OpcPuntualidad)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("opc_puntualidad")
					.HasDefaultValueSql("('')")
					.IsFixedLength();

			entity.Property(e => e.ImpVencido).HasColumnName("imp_vencido");

			entity.Property(e => e.PrcIngresomensual).HasColumnName("prc_ingresomensual");

			entity.Property(e => e.PrcPerfilgrupo1).HasColumnName("prc_perfilgrupo1");

			entity.Property(e => e.PrcPerfilgrupo2).HasColumnName("prc_perfilgrupo2");

			entity.Property(e => e.NumPuntosparcn).HasColumnName("num_puntosparcn");

			entity.Property(e => e.NumMesescra).HasColumnName("num_mesescra");

			entity.Property(e => e.NumFactorlinea).HasColumnName("num_factorlinea");

			entity.Property(e => e.NumEdadlrcmin).HasColumnName("num_edadlrcmin");

			entity.Property(e => e.ImpVencidolrcmin).HasColumnName("imp_vencidolrcmin");

			entity.Property(e => e.NumMesesprimercompralrcmin).HasColumnName("num_mesesprimercompralrcmin");

			entity.Property(e => e.NumMesesconsaldolrcmin).HasColumnName("num_mesesconsaldolrcmin");

			entity.Property(e => e.NumMesescongelados).HasColumnName("num_mesescongelados");

			entity.HasOne(d => d.BaseDatosOrigenNavigation)
					.WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ValidacionesLRC_Historial");
		});

		modelBuilder.Entity<CatPerfilriesgoLrc>(entity =>
		{
			entity
					.HasNoKey()
					.ToTable("cat_perfilriesgoLRC");

			entity.Property(e => e.IduPerfil).HasColumnName("idu_perfil");
			entity.Property(e => e.NumEdadfinal).HasColumnName("num_edadfinal");
			entity.Property(e => e.NumEdadinicial).HasColumnName("num_edadinicial");
			entity.Property(e => e.NumProceso)
					.HasDefaultValueSql("((1))")
					.HasColumnName("num_proceso");
			entity.Property(e => e.NumSmcvalormayor).HasColumnName("num_smcvalormayor");
			entity.Property(e => e.NumSmcvalormenor).HasColumnName("num_smcvalormenor");
			entity.Property(e => e.NumValormayor).HasColumnName("num_valormayor");
			entity.Property(e => e.NumValormenor).HasColumnName("num_valormenor");
		});

		modelBuilder.Entity<CatPerfilriesgoLrcHistorial>(entity =>
		{
			entity
					.HasNoKey()
					.ToTable("cat_perfilriesgoLRC_Historial");

			entity.Property(e => e.FechaAlta)
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta");
			entity.Property(e => e.IduPerfil).HasColumnName("idu_perfil");
			entity.Property(e => e.NumEdadfinal).HasColumnName("num_edadfinal");
			entity.Property(e => e.NumEdadinicial).HasColumnName("num_edadinicial");
			entity.Property(e => e.NumProceso)
					.HasDefaultValueSql("((1))")
					.HasColumnName("num_proceso");
			entity.Property(e => e.NumSmcvalormayor).HasColumnName("num_smcvalormayor");
			entity.Property(e => e.NumSmcvalormenor).HasColumnName("num_smcvalormenor");
			entity.Property(e => e.NumValormayor).HasColumnName("num_valormayor");
			entity.Property(e => e.NumValormenor).HasColumnName("num_valormenor");
		});

		modelBuilder.Entity<CtlPerfilRiesgoLrc>(entity =>
		{
			entity.HasKey(e => e.IdGrupo).HasName("PK__ctl_Perf__EC597A87BEFE32FE");

			entity.ToTable("ctl_PerfilRiesgoLRC");

			entity.Property(e => e.IdGrupo).HasColumnName("idGrupo");
			entity.Property(e => e.EdadMaximaPerfil).HasColumnName("edadMaximaPerfil");
			entity.Property(e => e.EdadMinimaPerfil).HasColumnName("edadMinimaPerfil");
			entity.Property(e => e.Estatus).HasColumnName("estatus");
			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");
			entity.Property(e => e.FechaAlta)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta");
			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");
			entity.Property(e => e.Grupo).HasColumnName("grupo");
			entity.Property(e => e.NumProceso)
					.HasDefaultValueSql("((1))")
					.HasColumnName("numProceso");
			entity.Property(e => e.SmcmaximoPerfil).HasColumnName("SMCMaximoPerfil");
			entity.Property(e => e.SmcminimoPerfil).HasColumnName("SMCMinimoPerfil");
			entity.Property(e => e.TopeMaximoPerfil).HasColumnName("topeMaximoPerfil");
			entity.Property(e => e.TopeMinimoPerfil).HasColumnName("topeMinimoPerfil");

			entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.CtlPerfilRiesgoLrcs)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_PerfilRiesgoLRC");
		});

		modelBuilder.Entity<CtlValidacionesLrc>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__ctl_Vali__3213E83F467E8129");

			entity.ToTable("ctl_ValidacionesLRC");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Estatus).HasColumnName("estatus");
			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");
			entity.Property(e => e.FechaAlta)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta");
			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");
			entity.Property(e => e.ImporteVencido).HasColumnName("importeVencido");
			entity.Property(e => e.NumMesesCalcularLrc).HasColumnName("numMesesCalcularLRC");
			entity.Property(e => e.NumMesesMinimoPrimeraCompra)
					.HasDefaultValueSql("((6))")
					.HasColumnName("numMesesMinimoPrimeraCompra");
			entity.Property(e => e.PrcCalcularCsa).HasColumnName("prcCalcularCSA");
			entity.Property(e => e.PrcPerfil1).HasColumnName("prcPerfil1");
			entity.Property(e => e.PrcPerfil2).HasColumnName("prcPerfil2");
			entity.Property(e => e.Puntualidad)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasDefaultValueSql("((0))")
					.IsFixedLength();
			entity.Property(e => e.TopeEdadMaxima)
					.HasDefaultValueSql("((75))")
					.HasColumnName("topeEdadMaxima");

			entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.CtlValidacionesLrcs)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_ValidacionesLRC");
		});

		modelBuilder.Entity<CtlCopiadoOrquestador>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__ctl_copi__3213E83FB1FCCAAA");

			entity.ToTable("ctl_copiadoOrquestador");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");
			entity.Property(e => e.FechaAlta)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta");
			entity.Property(e => e.Flag).HasColumnName("flag");
			entity.Property(e => e.NombreProceso)
					.HasMaxLength(150)
					.IsUnicode(false)
					.HasDefaultValueSql("('')")
					.HasColumnName("nombreProceso");
			entity.Property(e => e.NombreTabla)
					.HasMaxLength(80)
					.IsUnicode(false)
					.HasDefaultValueSql("('')")
					.HasColumnName("nombreTabla");
		});

		modelBuilder.Entity<CatParametrosasignacionlinea>(entity =>
		{
			entity
					.HasNoKey()
					.ToTable("cat_parametrosasignacionlinea");

			entity.Property(e => e.FecMovimiento)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime")
					.HasColumnName("fec_movimiento");
			entity.Property(e => e.IduLineadecredito).HasColumnName("idu_lineadecredito");
			entity.Property(e => e.IduPerfil).HasColumnName("idu_perfil");
			entity.Property(e => e.NomLineadecredito)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("nom_lineadecredito");
			entity.Property(e => e.NumLinearealfinal).HasColumnName("num_linearealfinal");
			entity.Property(e => e.NumLinearealinicial).HasColumnName("num_linearealinicial");
			entity.Property(e => e.NumValorperfil).HasColumnName("num_valorperfil");
		});

		modelBuilder.Entity<CatParametrosasignacionlineaHistorial>(entity =>
		{
			entity
					.HasNoKey()
					.ToTable("cat_parametrosasignacionlinea_Historial");

			entity.Property(e => e.FecMovimiento)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime")
					.HasColumnName("fec_movimiento");
			entity.Property(e => e.FechaAlta)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta");
			entity.Property(e => e.IduLineadecredito).HasColumnName("idu_lineadecredito");
			entity.Property(e => e.IduPerfil).HasColumnName("idu_perfil");
			entity.Property(e => e.NomLineadecredito)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("nom_lineadecredito");
			entity.Property(e => e.NumLinearealfinal).HasColumnName("num_linearealfinal");
			entity.Property(e => e.NumLinearealinicial).HasColumnName("num_linearealinicial");
			entity.Property(e => e.NumValorperfil).HasColumnName("num_valorperfil");

			entity.HasOne(d => d.BaseDatosOrigenNavigation).WithMany()
					.HasForeignKey(d => d.BaseDatosOrigen)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_catparametrosasignacionlineaHistorial");
		});

		modelBuilder.Entity<CtlAsignacionDeLinea>(entity =>
		{
			entity.HasKey(e => e.IdAsignacion).HasName("PK__ctl_Asig__E17144786F5B69AF");

			entity.ToTable("ctl_AsignacionDeLinea");

			entity.Property(e => e.IdAsignacion).HasColumnName("idAsignacion");
			entity.Property(e => e.Estatus).HasColumnName("estatus");
			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");
			entity.Property(e => e.FechaAlta)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta");
			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");
			entity.Property(e => e.IduLineadecredito).HasColumnName("idu_lineadecredito");
			entity.Property(e => e.IduPerfil).HasColumnName("idu_perfil");
			entity.Property(e => e.Rango).HasColumnName("rango");
			entity.Property(e => e.RangoMax).HasColumnName("rangoMax");
			entity.Property(e => e.RangoMin).HasColumnName("rangoMin");
			entity.Property(e => e.TipoLinea)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("tipoLinea");
			entity.Property(e => e.Valor).HasColumnName("valor");

			entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.CtlAsignacionDeLineas)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_AsignacionDeLinea");
		});

		modelBuilder.Entity<CtlAsignacionDeLinea>(entity =>
		{
			entity.HasKey(e => e.IdAsignacion).HasName("PK__ctl_Asig__E17144786F5B69AF");

			entity.ToTable("ctl_AsignacionDeLinea");

			entity.Property(e => e.IdAsignacion).HasColumnName("idAsignacion");
			entity.Property(e => e.Estatus).HasColumnName("estatus");
			entity.Property(e => e.FechaActualizacion)
					.HasColumnType("datetime")
					.HasColumnName("fechaActualizacion");
			entity.Property(e => e.FechaAlta)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime")
					.HasColumnName("fechaAlta");
			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");
			entity.Property(e => e.IduLineadecredito).HasColumnName("idu_lineadecredito");
			entity.Property(e => e.IduPerfil).HasColumnName("idu_perfil");
			entity.Property(e => e.Rango).HasColumnName("rango");
			entity.Property(e => e.RangoMax).HasColumnName("rangoMax");
			entity.Property(e => e.RangoMin).HasColumnName("rangoMin");
			entity.Property(e => e.TipoLinea)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("tipoLinea");
			entity.Property(e => e.Valor).HasColumnName("valor");

			entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.CtlAsignacionDeLineas)
					.HasForeignKey(d => d.Estatus)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("fk_ctl_AsignacionDeLinea");
		});

    modelBuilder.Entity<CtlAsignacionDeGrupo>(entity =>
    {
      entity.HasKey(e => e.Idterminaciones).HasName("PK__ctl_asig__9E0395CF7A2A952D");

      entity.ToTable("ctl_asignacionDeGrupo");

      entity.Property(e => e.Idterminaciones).HasColumnName("idterminaciones");
      entity.Property(e => e.Estatus).HasColumnName("estatus");
      entity.Property(e => e.FechaActualizacion)
          .HasColumnType("datetime")
          .HasColumnName("fechaActualizacion");
      entity.Property(e => e.FechaAlta)
          .HasDefaultValueSql("(getdate())")
          .HasColumnType("datetime")
          .HasColumnName("fechaAlta");
      entity.Property(e => e.FechaArranque)
          .HasDefaultValueSql("(getdate())")
          .HasColumnType("datetime")
          .HasColumnName("fechaArranque");
      entity.Property(e => e.Grupo)
          .HasMaxLength(256)
          .IsUnicode(false)
          .HasColumnName("grupo");
      entity.Property(e => e.Idgrupo).HasColumnName("idgrupo");
      entity.Property(e => e.Numproceso).HasColumnName("numproceso");
      entity.Property(e => e.Terminacioncliente)
          .HasMaxLength(256)
          .IsUnicode(false)
          .HasColumnName("terminacioncliente");
    });

    modelBuilder.Entity<CatTerminacionesgrupoasignacionLc>(entity =>
    {
      entity
          .HasNoKey()
          .ToTable("cat_terminacionesgrupoasignacionLC");

      entity.Property(e => e.DesGrupotestigo)
          .HasMaxLength(256)
          .IsUnicode(false)
          .HasColumnName("des_grupotestigo");
      entity.Property(e => e.FecMovimiento)
          .HasDefaultValueSql("(getdate())")
          .HasColumnType("datetime")
          .HasColumnName("fec_movimiento");
      entity.Property(e => e.IduGrupotestigo).HasColumnName("idu_grupotestigo");
      entity.Property(e => e.NumProceso).HasColumnName("num_proceso");
      entity.Property(e => e.NumTerminacioncliente)
          .HasMaxLength(256)
          .IsUnicode(false)
          .HasColumnName("num_terminacioncliente");
    });

    modelBuilder.Entity<CatTerminacionesgrupoasignacionLcHistorial>(entity =>
    {
      entity
          .HasNoKey()
          .ToTable("catTerminacionesgrupoasignacionLC_Historial");

      entity.Property(e => e.BaseDatosOrigen).HasColumnName("baseDatosOrigen");
      entity.Property(e => e.DesGrupotestigo)
          .HasMaxLength(256)
          .IsUnicode(false)
          .HasColumnName("des_grupotestigo");
      entity.Property(e => e.FecMovimiento)
          .HasDefaultValueSql("(getdate())")
          .HasColumnType("datetime")
          .HasColumnName("fec_movimiento");
      entity.Property(e => e.FechaAlta)
          .HasColumnType("datetime")
          .HasColumnName("fechaAlta");
      entity.Property(e => e.IduGrupotestigo).HasColumnName("idu_grupotestigo");
      entity.Property(e => e.NumProceso).HasColumnName("num_proceso");
      entity.Property(e => e.NumTerminacioncliente)
          .HasMaxLength(256)
          .IsUnicode(false)
          .HasColumnName("num_terminacioncliente");

      entity.HasOne(d => d.BaseDatosOrigenNavigation).WithMany()
          .HasForeignKey(d => d.BaseDatosOrigen)
          .OnDelete(DeleteBehavior.ClientSetNull)
          .HasConstraintName("fk_catTerminacionesgrupoasignacionLC_Historial");
    });

    modelBuilder.Entity<CtlCarterasFactoreSaturacion>(entity =>
    {
      entity.HasKey(e => e.IduCartera);

      entity.ToTable("ctl_carteras_factore_saturacion");

      entity.Property(e => e.IduCartera)
          .ValueGeneratedNever()
          .HasColumnName("idu_cartera");
      entity.Property(e => e.Activo)
          .HasDefaultValueSql("((1))")
          .HasColumnName("activo");
      entity.Property(e => e.DescCartera).HasColumnName("desc_cartera");
    });

    modelBuilder.Entity<CatCiudadTipoRiesgo>(entity =>
    {
      entity.HasKey(e => e.IdCiudad).HasName("PK__catCiuda__AEA2A71B54B6C41F");

      entity.ToTable("catCiudadTipoRiesgo");

      entity.Property(e => e.IdCiudad)
          .ValueGeneratedNever()
          .HasColumnName("idCiudad");
      entity.Property(e => e.FechaActualizacion)
          .HasColumnType("datetime")
          .HasColumnName("fechaActualizacion");
      entity.Property(e => e.FechaAlta)
          .HasDefaultValueSql("(getdate())")
          .HasColumnType("datetime")
          .HasColumnName("fechaAlta");
      entity.Property(e => e.IdTipoRiesgo).HasColumnName("idTipoRiesgo");
    });

		modelBuilder.Entity<CatinteresesmoratoriosrevolventediariaAdp>(entity =>
		{
			entity
					.HasNoKey()
					.ToTable("catinteresesmoratoriosrevolventediaria_ADP");

			entity.Property(e => e.CtrlActualizacion).HasColumnName("ctrl_Actualizacion");
			entity.Property(e => e.FechaAlta)
					.HasDefaultValueSql("(getdate())")
					.HasColumnType("datetime")
					.HasColumnName("fecha_Alta");
			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fecha_Arranque");
			entity.Property(e => e.IduCuenta).HasColumnName("idu_cuenta");
			entity.Property(e => e.NumDiasvencidos).HasColumnName("num_diasvencidos");
			entity.Property(e => e.PrcInteresmoratorio).HasColumnName("prc_interesmoratorio");
		});

		modelBuilder.Entity<CtlEnvioDeCifrasControl>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("pk_EnvioDeCifrasControl");

			entity.ToTable("ctl_EnvioDeCifrasControl");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Estatus).HasColumnName("estatus");
			entity.Property(e => e.FechaArranque)
					.HasColumnType("datetime")
					.HasColumnName("fechaArranque");
			entity.Property(e => e.IdPuntodeconsumo).HasColumnName("id_puntodeconsumo");
			entity.Property(e => e.TipoEjecucion).HasColumnName("tipo_ejecucion");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	public virtual DbSet<CatBasesDeDato> CatBasesDeDatos { get; set; } = null!;
	public virtual DbSet<CatBonificacionesPrestamo> CatBonificacionesPrestamos { get; set; } = null!;
	public virtual DbSet<HisCatBonificacionesPrestamo> CatBonificacionesPrestamosHistorials { get; set; } = null!;
	public virtual DbSet<CatBonificacionescartera> CatBonificacionescarteras { get; set; } = null!;
	public virtual DbSet<HisCatBonificacionescartera> CatBonificacionescarterasHistorials { get; set; } = null!;
	public virtual DbSet<CatCiudade> CatCiudades { get; set; } = null!;
	public virtual DbSet<CatCiudadesHistorial> CatCiudadesHistorials { get; set; } = null!;
	public virtual DbSet<CatCorreosOperacione> CatCorreosOperaciones { get; set; } = null!;
	public virtual DbSet<CatCrbonificacionesHistorialCl> CatCrbonificacionesHistorialCls { get; set; } = null!;
	public virtual DbSet<CatCrconfiguracionesplazosAdp> CatCrconfiguracionesplazosAdps { get; set; } = null!;
	public virtual DbSet<CatCrconfiguracionesplazosHistorial> CatCrconfiguracionesplazosHistorials { get; set; } = null!;
	public virtual DbSet<CatCrinteresmoratorioAdp> CatCrinteresmoratorioAdps { get; set; } = null!;
	public virtual DbSet<CatCrinteresmoratorioHistorial> CatCrinteresmoratorioHistorials { get; set; } = null!;
	public virtual DbSet<CatFactorescartera> CatFactorescarteras { get; set; } = null!;
	public virtual DbSet<CatFactorescarterasHistorial> CatFactorescarterasHistorials { get; set; } = null!;
	public virtual DbSet<CatFactorescarterasTmp> CatFactorescarterasTmps { get; set; } = null!;
	public virtual DbSet<CatFactorportipoproducto> CatFactorportipoproductos { get; set; } = null!;
	public virtual DbSet<CatFactorportipoproductoHistorial> CatFactorportipoproductoHistorials { get; set; } = null!;
	public virtual DbSet<CatInteresesMoratorio> CatInteresesMoratorios { get; set; } = null!;
	public virtual DbSet<CatInteresesMoratoriosHistorial> CatInteresesMoratoriosHistorials { get; set; } = null!;
	public virtual DbSet<CatLimCredClientesNuevo> CatLimCredClientesNuevos { get; set; } = null!;
	public virtual DbSet<CatLimCredClientesNuevosHistorial> CatLimCredClientesNuevosHistorials { get; set; } = null!;
	public virtual DbSet<CatLogicaAsignacionQuebrantadaHistorial> CatLogicaAsignacionQuebrantadaHistorials { get; set; } = null!;
	public virtual DbSet<CatLogicaAsignacionQuebrantadum> CatLogicaAsignacionQuebrantada { get; set; } = null!;
	public virtual DbSet<CatNegocio> CatNegocios { get; set; } = null!;
	public virtual DbSet<CatNegociosHistorial> CatNegociosHistorials { get; set; } = null!;
	public virtual DbSet<CatSalariosMinimo> CatSalariosMinimos { get; set; } = null!;
	public virtual DbSet<CatSalariosMinimosHistorial> CatSalariosMinimosHistorials { get; set; } = null!;
	public virtual DbSet<CatSalariosminimosctasperdida> CatSalariosminimosctasperdidas { get; set; } = null!;
	public virtual DbSet<CatSalariosminimosctasperdidasHistorial> CatSalariosminimosctasperdidasHistorials { get; set; } = null!;
	public virtual DbSet<CatTasareestructura> CatTasareestructuras { get; set; } = null!;
	public virtual DbSet<CatTasareestructuraHistorial> CatTasareestructuraHistorials { get; set; } = null!;
	public virtual DbSet<CatTiposlineacredito> CatTiposlineacreditos { get; set; } = null!;
	public virtual DbSet<CtlBonificacionCartera> CtlBonificacionCarteras { get; set; } = null!;
	public virtual DbSet<CtlCatalogoPlazo> CtlCatalogoPlazos { get; set; } = null!;
	public virtual DbSet<CtlConversionLineadeCredito> CtlConversionLineadeCreditos { get; set; } = null!;
	public virtual DbSet<CtlDecrementosLineadeCredito> CtlDecrementosLineadeCreditos { get; set; } = null!;
	public virtual DbSet<CtlEstatusParametrico> CtlEstatusParametricos { get; set; } = null!;
	public virtual DbSet<CtlFactoresSaturacionCartera> CtlFactoresSaturacionCarteras { get; set; } = null!;
	public virtual DbSet<CtlInteresMoratorioDcp> CtlInteresMoratorioDcps { get; set; } = null!;
	public virtual DbSet<CtlInteresMoratorioporCartera> CtlInteresMoratorioporCarteras { get; set; } = null!;
	public virtual DbSet<CtlParametrosautenticacion> CtlParametrosautenticacions { get; set; } = null!;
	public virtual DbSet<CtlPuntosdeconsumo> CtlPuntosdeconsumos { get; set; } = null!;
	public virtual DbSet<CtlSalarioMinimoCp> CtlSalarioMinimoCps { get; set; } = null!;
	public virtual DbSet<CtlSalarioMinimoLc> CtlSalarioMinimoLcs { get; set; } = null!;
	public virtual DbSet<CtlTasaInteresCpprestamo> CtlTasaInteresCpprestamos { get; set; } = null!;
	public virtual DbSet<Ctl_TasasIntere> Ctl_TasasInteres { get; set; } = null!;
	public virtual DbSet<CtlTasasIntere> CtlTasasInteres { get; set; } = null!;
	public virtual DbSet<CtlTasasInteresCp> CtlTasasInteresCps { get; set; } = null!;
	public virtual DbSet<CtlTasasInteresCpc> CtlTasasInteresCpcs { get; set; } = null!;
	public virtual DbSet<Ctl_TasasInteresHistorial> Ctl_TasasInteresHistorials { get; set; } = null!;
	public virtual DbSet<CtlTasasInteresHistorial> CtlTasasInteresHistorials { get; set; } = null!;
	public virtual DbSet<ValidacionesLrc> ValidacionesLrcs { get; set; } = null!;
	public virtual DbSet<ValidacionesLrcHistorial> ValidacionesLrcHistorials { get; set; } = null!;
	public virtual DbSet<CatPerfilriesgoLrc> CatPerfilriesgoLrcs { get; set; } = null!;
	public virtual DbSet<CatPerfilriesgoLrcHistorial> CatPerfilriesgoLrcHistorials { get; set; } = null!;
	public virtual DbSet<CtlPerfilRiesgoLrc> CtlPerfilRiesgoLrcs { get; set; } = null!;
	public virtual DbSet<CtlValidacionesLrc> CtlValidacionesLrcs { get; set; } = null!;
	public virtual DbSet<CtlCopiadoOrquestador> CtlCopiadoOrquestadors { get; set; } = null!;
	public virtual DbSet<CatParametrosasignacionlinea> CatParametrosasignacionlineas { get; set; } = null!;
	public virtual DbSet<CatParametrosasignacionlineaHistorial> CatParametrosasignacionlineaHistorials { get; set; } = null!;
	public virtual DbSet<CtlAsignacionDeLinea> CtlAsignacionDeLineas { get; set; } = null!;
	public virtual DbSet<CtlAsignacionDeGrupo> CtlAsignacionDeGrupos { get; set; } = null!;
	public virtual DbSet<CatTerminacionesgrupoasignacionLc> CatTerminacionesgrupoasignacionLcs { get; set; } = null!;
	public virtual DbSet<CatTerminacionesgrupoasignacionLcHistorial> CatTerminacionesgrupoasignacionLcHistorials { get; set; } = null!;
	public virtual DbSet<CtlCarterasFactoreSaturacion> CtlCarterasFactoreSaturacions { get; set; } = null!;
	public virtual DbSet<CatCiudadTipoRiesgo> CatCiudadTipoRiesgos { get; set; } = null!;
	public virtual DbSet<CtlEnvioDeCifrasControl> CtlEnvioDeCifrasControls { get; set; } = null!;
	public virtual DbSet<CatinteresesmoratoriosrevolventediariaAdp> CatinteresesmoratoriosrevolventediariaAdps { get; set; } = null!;
}
