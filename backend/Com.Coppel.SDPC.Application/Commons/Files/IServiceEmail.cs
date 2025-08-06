using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Application.Models.Services;

namespace Com.Coppel.SDPC.Application.Commons.Files;

public interface IServiceEmail
{
	string GetAttachmentFilePath(PuntoDeConsumoVM puntoDeConsumo, AreaType type, string? cartera = null);

	bool SendEmail(EmailVM email);

	bool SendErrorMail(PuntoDeConsumoVM puntoDeConsumo, string concacts, string databaseName, string tableName);

	bool SendNewYearValidationMail(string puntoDeConsumo, string contacts, string databaseName, string tableName, string updateField, string cifraCambio);

	bool SendMailStatusReplication(PuntoDeConsumoVM puntoDeConsumo, string contacts, string tableName, string dataBase, DateTime fechaArranque, bool isSuccessful);

	bool SendMailStatusReplicationMoratorio(PuntoDeConsumoVM puntoDeConsumo, string contacts, string tableName, string dataBase, DateTime fechaArranque, bool isSuccessful);

	bool SendMailCarterasReplication(PuntoDeConsumoVM puntoDeConsumo, string contacts, string tableName, string dataBase, DateTime fechaArranque, bool isSucesful);

	bool SendMailErrorBackups(PuntoDeConsumoVM puntoDeConsumo, string concacts, string databaseNameOrigen, string tableName, string tableHistorial, string databaseNameDestino, DateTime fechaArranque);
}
