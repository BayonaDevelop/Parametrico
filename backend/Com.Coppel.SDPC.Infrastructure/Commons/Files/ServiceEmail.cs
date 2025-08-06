using Com.Coppel.SDPC.Application.Commons.Files;
using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Application.Models.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace Com.Coppel.SDPC.Infrastructure.Commons.Files;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1192:String literals should not be duplicated", Justification = "The amount of literals is too short")]
public class ServiceEmail(IConfiguration configuration) : IServiceEmail
{
	private readonly string EMAIL_USER = (Utils.IsInProduction() ? "appSettings:emailUser_prod" : "appSettings:emailUser");


	public string GetAttachmentFilePath(PuntoDeConsumoVM puntoDeConsumo, AreaType type, string? cartera = null)
	{
		string result;
		string label = string.Empty;
		string todayDate = DateTime.Now.ToString("yyyy-MM-dd");

		switch (type)
		{
			case AreaType.CALIDAD:
				label = "Calidad";
				break;
			case AreaType.CARTERA_EN_LINEA:
				label = "Cartera En Linea";
				break;
			case AreaType.ETL:
				label = "ETL";
				break;
		}

		switch (puntoDeConsumo.NomFuncionalidad)
		{
			case "Conversión de Linea de Crédito":
			case "Salario Mínimo de Linea de Crédito":
			case "Salario Mínimo de Cuentas Perdidas":
			case "Decrementos de Línea de Crédito":
			case "Tasas de Interés por Cartera/Plazo":
			case "Tasas de Interés por Cartera/Plazo/Ciudad (muebles)":
			case "Tasas de Interés Moratorio Diaria de la Cartera de Préstamos":
			case "Tasas de Interés Moratorio por Cartera":
			case "Factores de Saturación por Cartera":
			case "Bonificaciones":
				if (string.IsNullOrEmpty(cartera))
				{
					result = $"\\{puntoDeConsumo.NomFuncionalidad}[{label}]-{todayDate}.pdf";
					result = result.Replace("/", "-");
				}
				else
				{
					var param = "";
					if (!string.IsNullOrEmpty(cartera))
						param = cartera;

					result = $"\\{puntoDeConsumo.NomFuncionalidad}{param}-{todayDate}.pdf";
				}
				break;
			default:
				result = $"\\{puntoDeConsumo.NomFuncionalidad}[{cartera}]-{todayDate}.pdf";
				break;
		}

		return result;
	}

	public bool SendEmail(EmailVM email)
	{
		try
		{
			MailMessage message = new()
			{
				From = new MailAddress(configuration.GetValue<string>(EMAIL_USER)!)
			};
			message.To.Add(email.To);
			message.Subject = email.Subject;
			message.Body = email.Body;

			foreach (var file in email.Files)
			{
				message.Attachments.Add(new Attachment(file));
			}

			SmtpClient mailServer = new();

			if (Utils.IsInProduction())
			{
				mailServer = new SmtpClient
				{
					Host = configuration.GetValue<string>("appSettings:emailSmtpClient_prod")!,
					Port = int.Parse(configuration!.GetValue<string>("appSettings:emailSmtpPort_prod")!),
					EnableSsl = true,

					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(configuration.GetValue<string>(EMAIL_USER), configuration.GetValue<string>("appSettings:emailKey_prod"))
				};
			}
			else
			{
				mailServer = new SmtpClient
				{
					Host = configuration.GetValue<string>("appSettings:emailSmtpClient")!,
					Port = int.Parse(configuration!.GetValue<string>("appSettings:emailSmtpPort")!),
					EnableSsl = true,

					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(configuration.GetValue<string>(EMAIL_USER), configuration.GetValue<string>("appSettings:emailKey"))
				};
			}

			mailServer.Send(message);
			return true;
		}
		catch (Exception)
		{
			Debug.WriteLine($"El correo de {email.PuntoDeConsumo.NomFuncionalidad} no se envió.");
			throw;
		}
	}

	public bool SendErrorMail(PuntoDeConsumoVM puntoDeConsumo, string concacts, string databaseName, string tableName)
	{
		try
		{
			string body = $"<html>" +
					$"<body>" +
					$"<p>¡Buen día a todos!<br/><br/>" +
					$"Ha ocurrido un error durante la actualización de la tabla ({tableName}) para el proceso de<br/>({puntoDeConsumo.NomFuncionalidad}) en la base de datos ({databaseName}).<br/><br/>" +
					$"Favor de atender a la brevedad posible.<br/><br/>" +
					$"Saludos!<br/><br/>" +
					$"Atentamente<br/>" +
					$"Administrador de Datos Parametrizables</p>" +
					$"</body>" +
					$"</html>";

			MailMessage message = new()
			{
				From = new MailAddress(configuration.GetValue<string>(EMAIL_USER)!)
			};
			message.To.Add(concacts);
			message.Subject = $"Reporte de error en el administrador de datos ({puntoDeConsumo.NomFuncionalidad})";
			message.Body = body;
			message.IsBodyHtml = true;

			SmtpClient mailServer = new(configuration.GetValue<string>("appSettings:emailSmtpClient"), int.Parse(configuration.GetValue<string>("appSettings:emailSmtpPort")!))
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(configuration.GetValue<string>(EMAIL_USER), configuration.GetValue<string>("appSettings:emailKey"))
			};
			mailServer.Send(message);

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool SendNewYearValidationMail(string puntoDeConsumo, string contacts, string databaseName, string tableName, string updateField, string cifraCambio)
	{
		try
		{
			string bodySMCP = $"<html>" +
			$"<body>" +
					$"<p>Se actualiza registro en la tabla ({tableName}) para el proceso de ({puntoDeConsumo}) en la base de datos ({databaseName}) con la fecha: [\"{DateTime.Today.Year}-{01}-{01} 00:00:00 \"] y el campo de salario: {updateField} con el último valor del año anterior: ({cifraCambio}).</p>" +
					$"</body>" +
					$"</html>";

			string bodySMLC = $"<html>" +
			$"<body>" +
					$"<p>Se inserta nuevo registro en la tabla ({tableName}) para el proceso de ({puntoDeConsumo}) en la base de datos ({databaseName}) con la fecha: [\"{DateTime.Today.Year}-{01}-{01} 00:00:00 \"] y el campo de salario: {updateField} con los últimos valores del año anterior: ({cifraCambio}).</p>" +
					$"</body>" +
					$"</html>";

			MailMessage message = new()
			{
				From = new MailAddress(configuration.GetValue<string>(EMAIL_USER)!)
			};
			message.To.Add(contacts);
			message.Subject = $"Reporte de cambio de año en el administrador de datos para: {puntoDeConsumo}";
			if (tableName == "cat_salariosminimosctasperdidas")
			{
				message.Body = bodySMCP;

			}
			else
			{
				message.Body = bodySMLC;
			}
			message.IsBodyHtml = true;

			SmtpClient mailServer = new(configuration.GetValue<string>("appSettings:emailSmtpClient"), int.Parse(configuration.GetValue<string>("appSettings:emailSmtpPort")!))
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(configuration.GetValue<string>(EMAIL_USER), configuration.GetValue<string>("appSettings:emailKey"))
			};
			mailServer.Send(message);

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool SendMailStatusReplication(PuntoDeConsumoVM puntoDeConsumo, string contacts, string tableName, string dataBase, DateTime fechaArranque, bool isSuccessful)
	{
		try
		{
			string body = string.Empty;
			string subject = string.Empty;

			switch (isSuccessful)
			{
				case true:
					body = $"<p>¡Buen día a todos!<br/><br/>" +
						$"Se concluyó con éxito la actualización de los valores del parámetro ({puntoDeConsumo.NomFuncionalidad})  en las tablas ({tableName}) el día de hoy, favor de revisar.<br/><br/>" +
						$"Saludos!<br/><br/>" +
						$"Atentamente<br/> " +
						$"Administrador de Datos Parametrizables</p>";
					subject = $"Se realizó la actualización de las tablas de {dataBase} con los cambios del Administrador de Datos Parametrizables de ({puntoDeConsumo.NomFuncionalidad}) con fecha de arranque {fechaArranque:dd-MM-yyyy}";
					break;
				case false:
					body = $@"<p>Buen día!<br><br>Existen cambios de valores del Administrador de Datos Parametrizables de (<b>{puntoDeConsumo.NomFuncionalidad}</b>) con fecha de arranque <b>{fechaArranque.ToShortDateString()}</b> pendientes de actualizar en <b>{dataBase}</b>.<br>
							Se intentó actualizar las tablas de {dataBase} (<b>{tableName}</b>) pero no se ha hecho la actualización correspondiente de los movimientos con fecha previa al cambio de valores.<br>
							Favor de revisar y si es necesario ejecute la actualización de valores manualmente.<br><br>
							Saludos!<br><br>
							Atte:<br>
							Administrador de Datos Parámetricos.</p>";
					subject = $"Error en la actualización de Cartera Central para {puntoDeConsumo.NomFuncionalidad}.";
					break;
			}

			MailMessage message = new()
			{
				From = new MailAddress(configuration.GetValue<string>(EMAIL_USER)!)
			};
			message.To.Add(contacts);
			message.Subject = subject;
			message.Body = body;
			message.IsBodyHtml = true;

			SmtpClient mailServer = new(configuration.GetValue<string>("appSettings:emailSmtpClient"), int.Parse(configuration.GetValue<string>("appSettings:emailSmtpPort")!))
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(configuration.GetValue<string>(EMAIL_USER), configuration.GetValue<string>("appSettings:emailKey"))
			};
			mailServer.Send(message);

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}
	public bool SendMailStatusReplicationMoratorio(PuntoDeConsumoVM puntoDeConsumo, string contacts, string tableName, string dataBase, DateTime fechaArranque, bool isSuccessful)
	{
		try
		{
			string body = string.Empty;
			string subject = string.Empty;

			switch (isSuccessful)
			{
				case true:
					body = $"<p>¡Buen día a todos!<br/><br/>" +
						$"Se concluyó con éxito la actualización de los valores del parámetro ({puntoDeConsumo.NomFuncionalidad})  en las tablas ({tableName}) el día de hoy, favor de revisar.<br/><br/>" +
						$"Saludos!<br/><br/>" +
						$"Atentamente<br/> " +
						$"Administrador de Datos Parametrizables</p>";
					subject = $"Se realizó la actualización de las tablas de {dataBase} con los cambios del Administrador de Datos Parametrizables de ({puntoDeConsumo.NomFuncionalidad}) con fecha de arranque {fechaArranque:dd-MM-yyyy}";
					break;
				case false:
					body = $@"<p>Buen día a todos!<br/><br/>
							Se intentó actualizar las tablas de {dataBase} (<b>{tableName}</b>) pero no se ha hecho la actualización correspondiente de los movimientos con fecha previa al cambio de valores.<br>
							Favor de revisar y si es necesario ejecute la actualización de valores manualmente.<br><br>
							Saludos!<br><br>
							Atte:<br>
							Administrador de Datos Parámetricos.</p>";
					subject = $"Existen cambios de valores del Administrador de Datos Parametrizables de ({puntoDeConsumo.NomFuncionalidad}) con fecha de arranque {fechaArranque.ToShortDateString()} pendientes de actualizar en {dataBase}.";
					break;
			}

			MailMessage message = new()
			{
				From = new MailAddress(configuration.GetValue<string>(EMAIL_USER)!)
			};
			message.To.Add(contacts);
			message.Subject = subject;
			message.Body = body;
			message.IsBodyHtml = true;

			SmtpClient mailServer = new(configuration.GetValue<string>("appSettings:emailSmtpClient"), int.Parse(configuration.GetValue<string>("appSettings:emailSmtpPort")!))
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(configuration.GetValue<string>(EMAIL_USER), configuration.GetValue<string>("appSettings:emailKey"))
			};
			mailServer.Send(message);

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool SendMailCarterasReplication(PuntoDeConsumoVM puntoDeConsumo, string contacts, string tableName, string dataBase, DateTime fechaArranque, bool isSucesful)
	{
		try
		{
			string body = string.Empty;
			string subject = string.Empty;

			switch (isSucesful)
			{
				case true:
					subject = $"Se realizó la actualización de las tablas de carteras con los cambios del Administrador de Datos Parametrizables de {puntoDeConsumo.NomFuncionalidad} con fecha de arranque {fechaArranque.ToShortDateString()}";
					body = $@"<p>Buen día a todos!<br><br>Se concluyó con éxito la actualización de los valores del parámetro <b>{puntoDeConsumo.NomFuncionalidad}</b> en las tablas: (<b>{tableName}</b>), el día de hoy, favor de revisar.<br><br>
              Saludos!<br><br>
							Atentamente<br>
							Administrador de Datos Parametrizables.</p>";
					break;
				case false:
					subject = $"Reporte de error en el administrador de datos {puntoDeConsumo.NomFuncionalidad}.";
					body = $@"<p>¡Buen día a todos!<br><br>Ha ocurrido un error durante la actualización de la tabla: (<b>{tableName}</b>), para el proceso <b>{puntoDeConsumo.NomFuncionalidad}</b> en la base de datos {dataBase}.<br>
							Favor de atender a la brevedad posible.<br><br>
							Saludos!<br><br>
							Atentamente<br>
							Administrador de Datos Parametrizables.</p>";
					break;
			}

			MailMessage message = new()
			{
				From = new MailAddress(configuration.GetValue<string>(EMAIL_USER)!)
			};
			message.To.Add(contacts);
			message.Subject = subject;
			message.Body = body;
			message.IsBodyHtml = true;

			SmtpClient mailServer = new(configuration.GetValue<string>("appSettings:emailSmtpClient"), int.Parse(configuration.GetValue<string>("appSettings:emailSmtpPort")!))
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(configuration.GetValue<string>(EMAIL_USER), configuration.GetValue<string>("appSettings:emailKey"))
			};
			mailServer.Send(message);

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool SendMailErrorBackups(PuntoDeConsumoVM puntoDeConsumo, string concacts, string databaseNameOrigen, string tableName, string tableHistorial, string databaseNameDestino, DateTime fechaArranque)
	{
		string subject;

		try
		{
			string body = $"<html>" +
					$"<body>" +
					$"<p>¡Buen día a todos!<br/><br/>" +
					$"Se presentó un problema al intentar respaldar la información ({tableName}) de la bd: ({databaseNameOrigen}) a la tabla ({tableHistorial}) de la bd: ({databaseNameDestino}).<br/>" +
					$"<b> En consecuencia no se pudo hacer la actualización del catálogo mencionado anteriormente.</b> <br/><br/>" +
					$"Favor de revisar y si es necesario ejecute la actualización de valores manualmente.<br/><br/>" +
					$"Saludos!<br/><br/>" +
					$"Atentamente<br/>" +
					$"Administrador de Datos Parametrizables</p>" +
					$"</body>" +
					$"</html>";

			subject = $"Existen cambios de valores del Administrador de Datos Parametrizables de ({puntoDeConsumo.NomFuncionalidad}) con fecha de arranque {fechaArranque.ToShortDateString()} pendientes de actualizar en {databaseNameOrigen}.";

			MailMessage message = new()
			{
				From = new MailAddress(configuration.GetValue<string>(EMAIL_USER)!)
			};
			message.To.Add(concacts);
			message.Subject = subject;
			message.Body = body;
			message.IsBodyHtml = true;

			SmtpClient mailServer = new(configuration.GetValue<string>("appSettings:emailSmtpClient"), int.Parse(configuration.GetValue<string>("appSettings:emailSmtpPort")!))
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(configuration.GetValue<string>(EMAIL_USER), configuration.GetValue<string>("appSettings:emailKey"))
			};
			mailServer.Send(message);

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}
}
