namespace Com.Coppel.SDPC.Infrastructure.Commons;
public static class SystemMessages
{
	public static readonly string ERROR_TEST_DECRYPT			= @"No fue posible desencriptar las conexiones. La aplicación ha finalizado";
	public static readonly string ERROR_TEST_CONNECTION		= @"No fue posible establecer conexión con la base de datos Catalogos. La aplicación ha finalizado";
	public static readonly string ERROR_GET_TOKEN					= @"No se pudo obtener el token. La aplicación ha finalizado";
	public static readonly string SEPARADOR								= @"======================================================================================================";
	public static readonly string DAILY_HEADER						= @"= Procesos diarios ===================================================================================";
	public static readonly string SEPARADOR_SIMPLE				= @"------------------------------------------------------------------------------------------------------";
	public static readonly string ITERATION								= @"- Intento #{0}  ----------------------------------------------------------------------------------------";
	public static readonly string INICIO_PROCESO					= @"* Inicia el proceso de [{0}]";
	public static readonly string FIN_PROCESO							= @"* Finaliza el proceso de [{0}]";
	public static readonly string DESCARGA_PARAMETROS			= @"  -> Descargando parámetros a la tabla [{0}]";
	public static readonly string CENSADO_PARAMETROS			= @"  -> Censando de parámetros ... ";
	public static readonly string INICIA_BD								= @"  => Inicia el proceso de información en [{0}]";
	public static readonly string FINALIZA_BD							= @"  => Finaliza el proceso de información en [{0}]";
	public static readonly string PREPARAR_RESPALDO				= @"    ° Preparando respaldos de [{0}] en [{1}] ";
	public static readonly string RESPALDO								= @"    ° Respaldando datos de [{0}] ";
	public static readonly string ERROR_ACTUALIZAR_TABLA	= @"    ° Error al actualizar a la tabla [{0}]";
	public static readonly string SEND_MAIL								= @"  -> Se envió el correo de Cifras de Control de {0}";
	public static readonly string ERROR_SEND_MAIL					= @"  -> Error al enviar el correo de Cifras de Control de  {0}";
}
