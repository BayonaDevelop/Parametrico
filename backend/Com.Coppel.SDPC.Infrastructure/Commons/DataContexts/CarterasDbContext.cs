using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Core.Carteras;
using Microsoft.EntityFrameworkCore;

namespace Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;

public partial class CarterasDbContext : DbContext
{
	public CarterasDbContext() { }

	public CarterasDbContext(DbContextOptions<CarterasDbContext> options) : base(options) {}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer(Utils.GetConnectionStrings().Find(i => StringComparer.OrdinalIgnoreCase.Equals(i.Key, Enum.GetName(typeof(DatabaseType), DatabaseType.Carteras)!)).Value);
		}
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3251:Implementations should be provided for \"partial\" methods", Justification = "It was defined by EntityFrameworkCore Scaffold")]
	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1192:String literals should not be duplicated", Justification = "It was defined by EntityFrameworkCore Scaffold")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3251:Implementations should be provided for \"partial\" methods", Justification = "It was defined by EntityFrameworkCore Scaffold")]
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CarCifraControlOperacion>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("carCifraControlOperacion");

			entity.Property(e => e.AbonoCreditoYcasa).HasColumnName("AbonoCreditoYCasa");

			entity.Property(e => e.AbonosTa).HasColumnName("AbonosTA");

			entity.Property(e => e.AjusteAbonoBincBanCoppel).HasColumnName("AjusteAbonoBIncBanCoppel");

			entity.Property(e => e.AjusteAbonoBincMuebles).HasColumnName("AjusteAbonoBIncMuebles");

			entity.Property(e => e.AjusteAbonoBincNegocios).HasColumnName("AjusteAbonoBIncNegocios");

			entity.Property(e => e.AjusteAbonoBincPrestamos).HasColumnName("AjusteAbonoBIncPrestamos");

			entity.Property(e => e.AjusteAbonoBincRopa).HasColumnName("AjusteAbonoBIncRopa");

			entity.Property(e => e.AjusteAbonoBincTa).HasColumnName("AjusteAbonoBIncTA");

			entity.Property(e => e.AjustesCanclntTa).HasColumnName("AjustesCanclntTA");

			entity.Property(e => e.AjustesIntReestDeudaBcpl).HasColumnName("AjustesIntReestDeudaBCPl");

			entity.Property(e => e.AjustesIntReestTa).HasColumnName("AjustesIntReestTA");

			entity.Property(e => e.AjustesReestDeudaBcpl).HasColumnName("AjustesReestDeudaBCPL");

			entity.Property(e => e.AjustesReestTa).HasColumnName("AjustesReestTA");

			entity.Property(e => e.AjustesTa).HasColumnName("AjustesTA");

			entity.Property(e => e.AltasAfore)
					.HasMaxLength(4)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.AltasUh).HasColumnName("AltasUH");

			entity.Property(e => e.BonificacionAutomaticaTa).HasColumnName("BonificacionAutomaticaTA");

			entity.Property(e => e.BonificacionTa).HasColumnName("BonificacionTA");

			entity.Property(e => e.Clave)
					.HasMaxLength(1)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.ConvenioTa).HasColumnName("ConvenioTA");

			entity.Property(e => e.FechaCorte).HasColumnType("datetime");

			entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");

			entity.Property(e => e.ImpAbonosproteccionsalud).HasColumnName("imp_abonosproteccionsalud");

			entity.Property(e => e.ImpAbonovtassegbcpl).HasColumnName("imp_abonovtassegbcpl");

			entity.Property(e => e.ImpAdicionalesproteccionsalud).HasColumnName("imp_adicionalesproteccionsalud");

			entity.Property(e => e.ImpBeneficiariosproteccionsalud).HasColumnName("imp_beneficiariosproteccionsalud");

			entity.Property(e => e.ImpBonificacionRefacturacion).HasColumnName("imp_BonificacionRefacturacion");

			entity.Property(e => e.ImpCancadicionalesproteccionsalud).HasColumnName("imp_cancadicionalesproteccionsalud");

			entity.Property(e => e.ImpDevolucionTarjetaContenidoActivas).HasColumnName("imp_devolucionTarjetaContenidoActivas");

			entity.Property(e => e.ImpDevolucionTarjetaContenidoInactivas).HasColumnName("imp_devolucionTarjetaContenidoInactivas");

			entity.Property(e => e.ImpDevolucionesrevolvente).HasColumnName("imp_devolucionesrevolvente");

			entity.Property(e => e.ImpIntcondonadoMuebles).HasColumnName("imp_intcondonadoMuebles");

			entity.Property(e => e.ImpIntcondonadoPrestamos).HasColumnName("imp_intcondonadoPrestamos");

			entity.Property(e => e.ImpIntcondonadoRevolvente).HasColumnName("imp_intcondonadoRevolvente");

			entity.Property(e => e.ImpIntcondonadoRopa).HasColumnName("imp_intcondonadoRopa");

			entity.Property(e => e.ImpIntcondonadoTa).HasColumnName("imp_intcondonadoTA");

			entity.Property(e => e.ImpInteresRefacturacion).HasColumnName("imp_InteresRefacturacion");

			entity.Property(e => e.ImpPagoTc).HasColumnName("imp_pagoTC");

			entity.Property(e => e.ImpPagoTcconcargoTd).HasColumnName("imp_pagoTCconcargoTD");

			entity.Property(e => e.ImpPagosTcconcargoTd).HasColumnName("imp_pagosTCconcargoTD");

			entity.Property(e => e.ImpPagovtassegbcpl).HasColumnName("imp_pagovtassegbcpl");

			entity.Property(e => e.ImpPolizasproteccionsalud).HasColumnName("imp_polizasproteccionsalud");

			entity.Property(e => e.ImpRetirosTc).HasColumnName("imp_retirosTC");

			entity.Property(e => e.ImpRetirosTd).HasColumnName("imp_retirosTD");

			entity.Property(e => e.ImpTarjetascontenido).HasColumnName("imp_tarjetascontenido");

			entity.Property(e => e.ImpTarjetascontenidoemp).HasColumnName("imp_tarjetascontenidoemp");

			entity.Property(e => e.ImpTraspasosTd).HasColumnName("imp_traspasosTD");

			entity.Property(e => e.ImpVentasproteccionsalud).HasColumnName("imp_ventasproteccionsalud");

			entity.Property(e => e.InteresCanceladoTa).HasColumnName("InteresCanceladoTA");

			entity.Property(e => e.InteresCargadoTa).HasColumnName("InteresCargadoTA");

			entity.Property(e => e.InteresCobradoTa).HasColumnName("InteresCobradoTA");

			entity.Property(e => e.InteresNoActualizadoTa).HasColumnName("InteresNoActualizadoTA");

			entity.Property(e => e.MarcaCargado)
					.HasMaxLength(1)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.MovtosNoActTa).HasColumnName("MovtosNoActTA");

			entity.Property(e => e.MovtosNoActualizadosUh).HasColumnName("MovtosNoActualizadosUH");

			entity.Property(e => e.NumAbonosproteccionsalud).HasColumnName("num_abonosproteccionsalud");

			entity.Property(e => e.NumAdicionalesproteccionsalud).HasColumnName("num_adicionalesproteccionsalud");

			entity.Property(e => e.NumAltasbeneficiarios).HasColumnName("num_altasbeneficiarios");

			entity.Property(e => e.NumAltasporabonosseguroscps).HasColumnName("num_altasporabonosseguroscps");

			entity.Property(e => e.NumAltasporcambiosau).HasColumnName("num_altasporcambiosau");

			entity.Property(e => e.NumAltasporcambioscoppel).HasColumnName("num_altasporcambioscoppel");

			entity.Property(e => e.NumAltasprospecto).HasColumnName("num_altasprospecto");

			entity.Property(e => e.NumBeneficiariosproteccionsalud).HasColumnName("num_beneficiariosproteccionsalud");

			entity.Property(e => e.NumCancadicionalesproteccionsalud).HasColumnName("num_cancadicionalesproteccionsalud");

			entity.Property(e => e.NumCancelacionesctes).HasColumnName("num_cancelacionesctes");

			entity.Property(e => e.NumCancelacionesref).HasColumnName("num_cancelacionesref");

			entity.Property(e => e.NumCancelacionessol).HasColumnName("num_cancelacionessol");

			entity.Property(e => e.NumConsultasaldoTc).HasColumnName("num_consultasaldoTC");

			entity.Property(e => e.NumConsultasaldoTd).HasColumnName("num_consultasaldoTD");

			entity.Property(e => e.NumFinalseguroscps).HasColumnName("num_finalseguroscps");

			entity.Property(e => e.NumIntcondonadoMuebles).HasColumnName("num_intcondonadoMuebles");

			entity.Property(e => e.NumIntcondonadoPrestamos).HasColumnName("num_intcondonadoPrestamos");

			entity.Property(e => e.NumIntcondonadoRevolvente).HasColumnName("num_intcondonadoRevolvente");

			entity.Property(e => e.NumIntcondonadoRopa).HasColumnName("num_intcondonadoRopa");

			entity.Property(e => e.NumIntcondonadoTa).HasColumnName("num_intcondonadoTA");

			entity.Property(e => e.NumMovcambiosctesituacion).HasColumnName("num_movcambiosctesituacion");

			entity.Property(e => e.NumMovcambiossolsituacion).HasColumnName("num_movcambiossolsituacion");

			entity.Property(e => e.NumMovcambiossolsituacionau).HasColumnName("num_movcambiossolsituacionau");

			entity.Property(e => e.NumMovcatsituaciones).HasColumnName("num_movcatsituaciones");

			entity.Property(e => e.NumMovdesmarcadoctesituacioninterno).HasColumnName("num_movdesmarcadoctesituacioninterno");

			entity.Property(e => e.NumMovdesmarcadosolsituacionau).HasColumnName("num_movdesmarcadosolsituacionau");

			entity.Property(e => e.NumMovdesmarcadosolsituacioninterno).HasColumnName("num_movdesmarcadosolsituacioninterno");

			entity.Property(e => e.NumMovmarcadoctesituacioninterno).HasColumnName("num_movmarcadoctesituacioninterno");

			entity.Property(e => e.NumMovmarcadosolsituacionau).HasColumnName("num_movmarcadosolsituacionau");

			entity.Property(e => e.NumMovmarcadosolsituacioninterno).HasColumnName("num_movmarcadosolsituacioninterno");

			entity.Property(e => e.NumMovregistrossolsituacionau).HasColumnName("num_movregistrossolsituacionau");

			entity.Property(e => e.NumMovreglasituaciones).HasColumnName("num_movreglasituaciones");

			entity.Property(e => e.NumMovsituacionesdesmarcarcte).HasColumnName("num_movsituacionesdesmarcarcte");

			entity.Property(e => e.NumMovsituacionesdesmarcarsol).HasColumnName("num_movsituacionesdesmarcarsol");

			entity.Property(e => e.NumMovsituacionesmarcarcte).HasColumnName("num_movsituacionesmarcarcte");

			entity.Property(e => e.NumMovsituacionesmarcarsol).HasColumnName("num_movsituacionesmarcarsol");

			entity.Property(e => e.NumMovsituacionesnoactcte).HasColumnName("num_movsituacionesnoactcte");

			entity.Property(e => e.NumMovsituacionesnoactsol).HasColumnName("num_movsituacionesnoactsol");

			entity.Property(e => e.NumMovsituacionesnoactsolau).HasColumnName("num_movsituacionesnoactsolau");

			entity.Property(e => e.NumOposicionesctes).HasColumnName("num_oposicionesctes");

			entity.Property(e => e.NumOposicionesref).HasColumnName("num_oposicionesref");

			entity.Property(e => e.NumOposicionessol).HasColumnName("num_oposicionessol");

			entity.Property(e => e.NumPagoTc).HasColumnName("num_pagoTC");

			entity.Property(e => e.NumPagoTcconcargoTd).HasColumnName("num_pagoTCconcargoTD");

			entity.Property(e => e.NumPagosTcconcargoTd).HasColumnName("num_pagosTCconcargoTD");

			entity.Property(e => e.NumPolizasproteccionsalud).HasColumnName("num_polizasproteccionsalud");

			entity.Property(e => e.NumRegistrossituacionescte).HasColumnName("num_registrossituacionescte");

			entity.Property(e => e.NumRegistrossituacionessol).HasColumnName("num_registrossituacionessol");

			entity.Property(e => e.NumRetirosTc).HasColumnName("num_retirosTC");

			entity.Property(e => e.NumRetirosTd).HasColumnName("num_retirosTD");

			entity.Property(e => e.NumSeguroscanceladoscps).HasColumnName("num_seguroscanceladoscps");

			entity.Property(e => e.NumSegurosdepuradoscps).HasColumnName("num_segurosdepuradoscps");

			entity.Property(e => e.NumTotalMigraciones).HasColumnName("num_totalMigraciones");

			entity.Property(e => e.NumTraspasosTd).HasColumnName("num_traspasosTD");

			entity.Property(e => e.NumVentasproteccionsalud).HasColumnName("num_ventasproteccionsalud");

			entity.Property(e => e.NumVtasSegbcpl).HasColumnName("num_vtasSegbcpl");

			entity.Property(e => e.PagoInteresTa).HasColumnName("PagoInteresTA");

			entity.Property(e => e.ReembolsoTa).HasColumnName("ReembolsoTA");

			entity.Property(e => e.RegistrosUh).HasColumnName("RegistrosUH");

			entity.Property(e => e.SaldoFinalTa20anterior).HasColumnName("SaldoFinalTA20Anterior");

			entity.Property(e => e.SaldoRojoMensualTa).HasColumnName("SaldoRojoMensualTA");

			entity.Property(e => e.VentaCteContadoTa).HasColumnName("VentaCteContadoTA");

			entity.Property(e => e.VentaCteTa).HasColumnName("VentaCteTA");

			entity.Property(e => e.VentaEmpContadoTa).HasColumnName("VentaEmpContadoTA");

			entity.Property(e => e.VentaEmpTa).HasColumnName("VentaEmpTA");

			entity.Property(e => e.VentaEngancheCteTa).HasColumnName("VentaEngancheCteTA");

			entity.Property(e => e.VentaEngancheEmpTa).HasColumnName("VentaEngancheEmpTA");
		});

		modelBuilder.Entity<CatBonificacionesPrestamo>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("cat_bonificacionesPrestamos");

			entity.Property(e => e.FecCorte)
					.HasColumnType("date")
					.HasColumnName("fec_corte")
					.HasComment("Fecha corte");

			entity.Property(e => e.FecMovimiento)
					.HasColumnType("smalldatetime")
					.HasColumnName("fec_movimiento")
					.HasComment("Fecha en la que se realizo el movimiento")
					.HasDefaultValueSql("(CONVERT([smalldatetime],getdate(),(0)))");

			entity.Property(e => e.NumDiasTranscurridos)
					.HasColumnName("num_diasTranscurridos")
					.HasComment("Numero de dias transcurridos");

			entity.Property(e => e.NumPlazo)
					.HasColumnName("num_plazo")
					.HasDefaultValueSql("((0))")
					.HasComment("Numero de Plazos");

			entity.Property(e => e.PrcBonificacion)
					.HasColumnType("numeric(10, 4)")
					.HasColumnName("prc_bonificacion")
					.HasComment("Porcentaje de Bonificacion");

			entity.Property(e => e.PrcBonificacionNueva)
					.HasColumnType("numeric(10, 4)")
					.HasColumnName("prc_bonificacionNueva")
					.HasComment("Porcentaje de Bonificacion Nueva");
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

		modelBuilder.Entity<CatCiudade>(entity =>
		{
			entity.HasNoKey();

			entity.Property(e => e.AntiguedadCiudad).HasColumnType("smalldatetime");

			entity.Property(e => e.FechaUltimaActualizacion).HasColumnType("smalldatetime");

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
			entity.HasKey(e => new { e.IduCartera, e.ClvPlazo, e.ClvLineacreditoespecial })
					.HasName("PK_cat_FactoresCarteras");

			entity.ToTable("cat_factorescarteras");

			entity.Property(e => e.IduCartera)
					.HasColumnName("idu_cartera")
					.HasComment("Identificador de la cartera");

			entity.Property(e => e.ClvPlazo)
					.HasColumnName("clv_plazo")
					.HasComment("Identificador del plazo");

			entity.Property(e => e.ClvLineacreditoespecial)
					.HasColumnName("clv_lineacreditoespecial")
					.HasComment("Identifica si es cliente normal o tiene credito especial");

			entity.Property(e => e.PrcFactor)
					.HasColumnType("numeric(18, 2)")
					.HasColumnName("prc_factor")
					.HasComment("Factor de la cartera");
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

		modelBuilder.Entity<CatFecha>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("catFechas");

			entity.Property(e => e.Fecha).HasColumnType("smalldatetime");

			entity.Property(e => e.FechaAnterior).HasColumnType("smalldatetime");
		});

		modelBuilder.Entity<CatInteresesMoratorio>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("catInteresesMoratorios");

			entity.Property(e => e.PorcentajeInteresMoratorio)
					.HasMaxLength(8)
					.IsUnicode(false)
					.IsFixedLength();
		});

		modelBuilder.Entity<CatLimCredClientesNuevo>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("catLimCredClientesNuevos");
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
					.HasColumnName("fechaAlta");

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

		modelBuilder.Entity<CatProductoreestructura>(entity =>
		{
			entity.HasKey(e => e.IduProducto)
					.HasName("PK__cat_prod__C15FF4DEE3FB89EC");

			entity.Property(e => e.IduProducto)
					.ValueGeneratedNever()
					.HasColumnName("idu_Producto")
					.HasComment("Identificador del producto de reestructura de crédito, es auto incrementable.");

			entity.Property(e => e.FecCreacion)
					.HasColumnType("date")
					.HasColumnName("fec_creacion")
					.HasComment("fecha en la que se registra el producto de reestructura");

			entity.Property(e => e.FecModificacion)
					.HasColumnType("date")
					.HasColumnName("fec_Modificacion")
					.HasComment("Fecha en la que sufrió cambio la configuración del producto.");

			entity.Property(e => e.ImpSaldoMinimo)
					.HasColumnName("imp_SaldoMinimo")
					.HasComment("Es la cantidad mínima que el cliente debe tener como saldo total para tener acceso a la reestructura.");

			entity.Property(e => e.NomProducto)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("nom_Producto")
					.HasDefaultValueSql("('')")
					.HasComment("Nombre del producto de la reestructura.");

			entity.Property(e => e.NumAbonoDesbloqueo)
					.HasColumnName("num_AbonoDesbloqueo")
					.HasComment("Cuantos abonos debe dar el cliente para que pueda hacer nuevas compras.");

			entity.Property(e => e.NumAbonoNormalizaCredito)
					.HasColumnName("num_AbonoNormalizaCredito")
					.HasDefaultValueSql("((0))")
					.HasComment("A partir de cuantos abonos cumplidos puntualmente, el cliente puede jugar nuevamente en los cambios de puntualidad.");

			entity.Property(e => e.NumUltimoAbono)
					.HasColumnName("num_ultimoAbono")
					.HasComment("Indica el número de meses que no deben pasar sin que el cliente registre un abono para poder ofrecer la reestructuración.");

			entity.Property(e => e.OpcClienteConflicto)
					.HasColumnName("opc_ClienteConflicto")
					.HasComment("Indica que si un cliente catalogado como conflicto puede ser candidato al producto de reestructura.");

			entity.Property(e => e.OpcCondicionPago)
					.IsRequired()
					.HasColumnName("opc_CondicionPago")
					.HasDefaultValueSql("((1))")
					.HasComment("Indica si el sistema debe solicitar al cliente dar el abono inicial para formalizar la reestructura del crédito.");

			entity.Property(e => e.OpcCondonacionMoratorio)
					.IsRequired()
					.HasColumnName("opc_condonacionMoratorio")
					.HasDefaultValueSql("((1))")
					.HasComment("Si la reestructura perdona los moratorios no pagados.");

			entity.Property(e => e.OpcEstatus)
					.IsRequired()
					.HasColumnName("opc_Estatus")
					.HasDefaultValueSql("((1))")
					.HasComment("Indica si el producto de reestructuración esta activó.");

			entity.Property(e => e.OpcFormula)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("opc_Formula")
					.HasDefaultValueSql("('A')")
					.IsFixedLength()
					.HasComment("Identifica la lógica o formula para calcular el importe a financiar para la reestructura del crédito. ");
		});

		modelBuilder.Entity<CatSalariosMinimo>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("catSalariosMinimos");

			entity.Property(e => e.FechaSalario).HasColumnType("smalldatetime");
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

		modelBuilder.Entity<CatTasareestructura>(entity =>
		{
			entity.HasKey(e => new { e.IduProducto, e.NumPlazo })
					.HasName("PK__cat_tasa__3B50757D69C0F37C");

			entity.ToTable("cat_tasareestructura");

			entity.Property(e => e.IduProducto).HasColumnName("idu_Producto");

			entity.Property(e => e.NumPlazo).HasColumnName("num_plazo");

			entity.Property(e => e.NumTasa).HasColumnName("num_tasa");

			entity.HasOne(d => d.IduProductoNavigation)
					.WithMany(p => p.CatTasareestructuras)
					.HasForeignKey(d => d.IduProducto)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_cat_tasareestructura_cat_productoreestructura");
		});

		modelBuilder.Entity<Catnegocio>(entity =>
		{
			entity.HasKey(e => e.ClaveNegocio)
					.HasName("PK_Negocios");

			entity.ToTable("catnegocios");

			entity.Property(e => e.ClaveNegocio).ValueGeneratedNever();

			entity.Property(e => e.Comision)
					.HasColumnType("decimal(19, 4)")
					.HasColumnName("comision");

			entity.Property(e => e.DescripcionNegocio)
					.HasMaxLength(100)
					.IsUnicode(false);

			entity.Property(e => e.FechaApertura).HasColumnType("smalldatetime");

			entity.Property(e => e.InicialNegocio)
					.HasMaxLength(4)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.Iva).HasColumnName("iva");

			entity.Property(e => e.NegocioActivo)
					.HasMaxLength(1)
					.IsUnicode(false)
					.IsFixedLength();

			entity.Property(e => e.PrcTasaInteres)
					.HasColumnName("prc_TasaInteres")
					.HasDefaultValueSql("((0))");

			entity.Property(e => e.Subcuenta).HasColumnName("subcuenta");
		});

		modelBuilder.Entity<CtlTasasIntere1>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("ctlTasasInteres");

			entity.Property(e => e.Fecha).HasColumnType("datetime");

			entity.Property(e => e.TasaPrestamosPlazo18).HasColumnType("numeric(18, 2)");

			entity.Property(e => e.TasaPrestamosPlazo24).HasColumnType("numeric(18, 2)");
		});

		modelBuilder.Entity<CtlTasasintere>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("ctl_tasasinteres");

			entity.Property(e => e.ClvAltoriesgo).HasColumnName("clv_altoriesgo");

			entity.Property(e => e.ClvCelulares).HasColumnName("clv_celulares");

			entity.Property(e => e.ClvEspecial).HasColumnName("clv_especial");

			entity.Property(e => e.DesTasainteres)
					.HasMaxLength(50)
					.IsUnicode(false)
					.HasColumnName("des_tasainteres")
					.HasDefaultValueSql("('')");

			entity.Property(e => e.IduCartera).HasColumnName("idu_cartera");

			entity.Property(e => e.IduPlazo).HasColumnName("idu_plazo");

			entity.Property(e => e.IduTipotasa).HasColumnName("idu_tipotasa");

			entity.Property(e => e.PrcTasainteres)
					.HasColumnType("numeric(10, 2)")
					.HasColumnName("prc_tasainteres");
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

		OnModelCreatingPartial(modelBuilder);
	}

	public virtual DbSet<CarCifraControlOperacion> CarCifraControlOperacions { get; set; } = null!;
	public virtual DbSet<CatBonificacionesPrestamo> CatBonificacionesPrestamos { get; set; } = null!;
	public virtual DbSet<CatBonificacionescartera> CatBonificacionescarteras { get; set; } = null!;
	public virtual DbSet<CatCiudade> CatCiudades { get; set; } = null!;
	public virtual DbSet<CatFactorescartera> CatFactorescarteras { get; set; } = null!;
	public virtual DbSet<CatFactorportipoproducto> CatFactorportipoproductos { get; set; } = null!;
	public virtual DbSet<CatFecha> CatFechas { get; set; } = null!;
	public virtual DbSet<CatInteresesMoratorio> CatInteresesMoratorios { get; set; } = null!;
	public virtual DbSet<CatLimCredClientesNuevo> CatLimCredClientesNuevos { get; set; } = null!;
	public virtual DbSet<CatLogicaAsignacionQuebrantadum> CatLogicaAsignacionQuebrantada { get; set; } = null!;
	public virtual DbSet<CatNegociosHistorial> CatNegociosHistorials { get; set; } = null!;
	public virtual DbSet<CatProductoreestructura> CatProductoreestructuras { get; set; } = null!;
	public virtual DbSet<CatSalariosMinimo> CatSalariosMinimos { get; set; } = null!;
	public virtual DbSet<CatSalariosminimosctasperdida> CatSalariosminimosctasperdidas { get; set; } = null!;
	public virtual DbSet<CatTasareestructura> CatTasareestructuras { get; set; } = null!;
	public virtual DbSet<Catnegocio> Catnegocios { get; set; } = null!;
	public virtual DbSet<CtlTasasIntere1> CtlTasasInteres1 { get; set; } = null!;
	public virtual DbSet<CtlTasasintere> CtlTasasinteres { get; set; } = null!;
	public virtual DbSet<ValidacionesLrc> ValidacionesLrcs { get; set; } = null!;
	public virtual DbSet<CatPerfilriesgoLrc> CatPerfilriesgoLrcs { get; set; } = null!;
	public virtual DbSet<CatParametrosasignacionlinea> CatParametrosasignacionlineas { get; set; } = null!;
	public virtual DbSet<CatTerminacionesgrupoasignacionLc> CatTerminacionesgrupoasignacionLcs { get; set; } = null!;
}
