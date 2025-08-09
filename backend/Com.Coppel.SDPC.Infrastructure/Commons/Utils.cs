using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;
using iText.Kernel.Crypto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Serilog;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Infrastructure.Commons;

public static class Utils
{
	private static readonly string _basePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)!.Replace("file:\\", "")!;

	private static int GetPropertyIndex(PropertyDescriptorCollection properties, string property)
	{
		for (int i = 0; i < properties.Count; i++)
		{
			if (properties[i].DisplayName.Equals(property))
			{
				return i;
			}
		}

		return -1;
	}

	private static DbContext GetDbContext(Type table)
	{
		var namespaceParts = table.FullName!.Split('.');
		var connectionStringName = namespaceParts[4];

		DbContext result = connectionStringName switch
		{
			"Carteras" => new CarterasDbContext(),
			"Cat" => new CatDbContext(),
			"ControlTiendas" => new ControlTiendasDbContext(),
			"ListadosCobranza" => new ListadosCobranzaDbContext(),
			"Emision20" => new Emision20DbContext(),
			_ => new CatalogosDbContext(),
		};

		return result;
	}

	public static IConfiguration GetConfiguration
	{
		get
		{
			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appSettings.json", optional: false, reloadOnChange: false)
			.Build();

			return builder;
		}
	}

	public static bool IsInProduction()
	{
		Profile profile = (Profile)short.Parse(GetConfiguration.GetValue<string>("ASPNETCORE_ENVIRONMENT")!);
		return profile == Profile.PRODUCTION;
	}

	public static bool IsInTesting()
	{
		Profile profile = (Profile)short.Parse(GetConfiguration.GetValue<string>("ASPNETCORE_ENVIRONMENT")!);
		return profile == Profile.TEST;
	}

	public static string GetProfileName()
	{
		string result = string.Empty;
		Profile profile = (Profile)short.Parse(GetConfiguration.GetValue<string>("ASPNETCORE_ENVIRONMENT")!);

		switch (profile)
		{
			case Profile.PRODUCTION:
				result = "Production";
				break;

			case Profile.DEVELOP:
				result = "Develop";
				break;

			case Profile.TEST:
				result = "Test";
				break;
		}

		return result;
	}

	public static List<string> GetClassHeaders(Type classType)
	{
		List<string> result = [];
		BindingFlags instancePublicAndNot = BindingFlags.Instance |
						BindingFlags.Public;

		var memberNames = classType
				.GetProperties(instancePublicAndNot)
				.OfType<MemberInfo>()
				.Union(classType.GetFields(instancePublicAndNot))
				.Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute))
								 && !Attribute.IsDefined(x, typeof(NonSerializedAttribute)))
				.Select(x => x.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DataMemberAttribute))?.NamedArguments.FirstOrDefault(n => n.MemberName == "Name").TypedValue.Value ?? x.Name);

		foreach (var memberName in memberNames)
		{
			result.Add(memberName.ToString()!);
		}

		return result;
	}

	public static DataTable ConvertDataToTable(Type classType, List<dynamic> data)
	{
		PropertyDescriptorCollection props = TypeDescriptor.GetProperties(classType);
		DataTable table = new();
		try
		{
			for (int i = 0; i < props.Count; i++)
			{
				PropertyDescriptor prop = props[i];
				DataColumn column = new()
				{
					ColumnName = prop.Name,
					DataType = prop.PropertyType,
				};
				table.Columns.Add(column);
			}

			object[] values = new object[props.Count];
			foreach (dynamic item in data)
			{
				for (int i = 0; i < table.Columns.Count; i++)
				{
					values[i] = props[GetPropertyIndex(props, table.Columns[i].ColumnName)].GetValue(item);
				}
				table.Rows.Add(values);
			}

			List<string> headers = GetClassHeaders(classType);
			for (int index = 0; index < headers.Count; index++)
			{
				table.Columns[index].ColumnName = headers[index];
			}
		}
		catch (Exception)
		{
			Debug.WriteLine("");
		}


		return table;
	}

	public static string CheckFile(PuntoDeConsumoVM puntoDeConsumo, string tableName, AreaType type, bool isExcel, bool extraFile = false)
	{
		string fileName = puntoDeConsumo.NomFuncionalidad.Equals("Bonificaciones") ? $"{puntoDeConsumo.NomFuncionalidad}-{tableName}" : puntoDeConsumo.NomFuncionalidad;
		string process = string.Empty;
		var fileDate = DateTime.Now.ToString("yyyy-MM-dd");

		switch (type)
		{
			case AreaType.CALIDAD:
				process = "Calidad";
				break;
			case AreaType.CARTERA_EN_LINEA:
				process = "Cartera En Linea";
				break;
			case AreaType.ETL:
				process = "ETL";
				break;
		}

		fileName = extraFile ? $"{fileName} [{process}]- {tableName} -ANTES-{fileDate}" : $"{fileName} [{process}]- {tableName} - {fileDate}";
		string folderName = isExcel ? "Excel" : "Pdf";
		string fileExtension = isExcel ? "xlsx" : "pdf";


		if (!Directory.Exists($"{_basePath}\\Archivos"))
		{
			Directory.CreateDirectory($"{_basePath}\\Archivos");
		}

		if (!Directory.Exists($"{_basePath}\\Archivos\\{folderName}"))
		{
			Directory.CreateDirectory($"{_basePath}\\Archivos\\{folderName}");
		}

		string result = $"{_basePath}\\Archivos\\{folderName}\\{fileName}.{fileExtension}";

		try
		{
			if (File.Exists(result))
			{
				File.Delete(result);
			}
		}
		catch (Exception)
		{
			Debug.WriteLine("");
			throw;
		}

		return result;
	}

	public static bool IsFileLocked(FileInfo file)
	{
		try
		{
			using FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
			stream.Close();
		}
		catch (IOException)
		{
			return true;
		}

		return false;
	}

	private static List<KeyValuePair<string, string>> ProcessConnectiosnJson(JToken? json)
	{
		List<KeyValuePair<string, string>> result = [];

		if (json != null)
		{
			foreach (JToken item in json.ToList())
			{
				JProperty jProperty = item.ToObject<JProperty>()!;
				if (jProperty != null)
				{
					var aux = System.Environment.Version;
					string dataBasePath = $"{_basePath.Replace("\\bin\\Debug\\net6.0", "")}\\Resources\\Databases";
					var connection = (!IsInTesting()) ?
						new KeyValuePair<string, string>(jProperty.Name, jProperty.Value.ToString()) :
						new KeyValuePair<string, string>(jProperty.Name, $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={dataBasePath}\\{jProperty.Name}.mdf;Integrated Security=True");
					result.Add(connection);
				}
			}
		}

		return result;
	}

	public static List<KeyValuePair<string, string>> GetConnectionStrings()
	{
		List<KeyValuePair<string, string>> connections = [];
		string path = Path.GetFullPath("./");
		string connectionsFilePath = Path.GetFullPath(Path.Combine(path, "connections.txt"));

		try
		{
			string decryptedText = AesCipher.DecryptFile(connectionsFilePath);

			var jobjet = JObject.Parse(decryptedText);
			if (jobjet != null)
			{
				JToken? json = jobjet.SelectToken("ConnectionStrings");
				connections = ProcessConnectiosnJson(json);
			}

			return connections;
		}
		catch (Exception)
		{
			Debug.WriteLine("So se pudo obtener la lista de conexiones");
			throw;
		}
	}

	public static string GetTableName(Type table)
	{
		var namespaceParts = table.FullName!.Split('.');
		var isPostgreSQL = namespaceParts[4].CompareTo("CarteraEnLinea") == 0;
		var dbContext = GetDbContext(table);
		var entityType = dbContext.Model.FindEntityType(table);
		var schemaText = isPostgreSQL ? "public" : "dbo";

		var schemaName = schemaText;
		var tableName = entityType!.GetTableName();

		return tableName == string.Empty ? string.Empty : $"{tableName}";
	}
}
