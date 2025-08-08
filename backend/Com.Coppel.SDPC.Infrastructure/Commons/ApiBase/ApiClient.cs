using Com.Coppel.SDPC.Application.Models.Enums;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Security.Authentication;
using System.Text;

namespace Com.Coppel.SDPC.Infrastructure.Commons.ApiBase;

public class ApiClient
{
	private const SslProtocols _tls12 = (SslProtocols)0x00000C00;
	private const SecurityProtocolType tls12 = (SecurityProtocolType)_tls12;
	protected string jwtToken;
	protected readonly JsonSerializerSettings jsonSerializerSettings;

	public ApiClient()
	{
		jwtToken = string.Empty;
		ServicePointManager.SecurityProtocol = tls12;
		jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
	}

	public static async Task<string> FetchTokenJsonFromPostAsync(string url, List<KeyValuePair<string, string>> headers, List<KeyValuePair<string, string>> formData, CancellationToken cancellationToken)
	{
		try
		{
			cancellationToken.ThrowIfCancellationRequested();

			string responseBody = string.Empty;
			string result = string.Empty;

			using (HttpClient client = new())
			{
				HttpRequestMessage request = new(HttpMethod.Post, url);

				foreach (var header in headers)
				{
					if (header.Key.CompareTo("Content-Type") != 0)
					{
						request.Headers.Add(header.Key, header.Value);
					}
				}

				request.Content = new FormUrlEncodedContent(formData);
				request.Content.Headers.ContentType!.MediaType = headers[5].Value;

				using HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(true);
				if (!response.IsSuccessStatusCode)
				{
					return string.Empty;
				}

				responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(true);
				JObject data = JObject.Parse(responseBody);
				result = data.SelectToken("access_token")!.Value<string>()!;
			}

			return result;
		}
		catch (Exception)
		{
			Debug.WriteLine("Error al obtener el token");
			throw;
		}
	}

	public static dynamic FetchStringJsonFromPostAsync(string url, object requestBody, CancellationToken cancellationToken)
	{
		try
		{
			cancellationToken.ThrowIfCancellationRequested();
			var client = new HttpClient();
			var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = client.PostAsync(url, content, cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult();

			if (response == null)
			{
				return ApiResultType.NO_DATA;
			}
			else if (!response.IsSuccessStatusCode)
			{
				return ApiResultType.URI_NOT_FOUND;
			}

			var result = response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult();

			if (result == null || result.Contains("Nombre De Cartera Incorrecto"))
			{
				return ApiResultType.NO_DATA;
			}


			JObject data = JObject.Parse(result);
			var token = data
					.SelectToken("data")!
					.SelectToken("token")!
					.Value<string>();

			return token!;
		}
		catch (Exception)
		{
			return null!;
		}
	}

	public async Task<dynamic> FetchStringJsonFromGetAsync(string url, string localData, CancellationToken cancellationToken)
	{
		try
		{
			cancellationToken.ThrowIfCancellationRequested();
			string result = string.Empty;

			if (Utils.IsInTesting())
			{
				result = localData;
			}
			else
			{
				HttpClient client = new();
				client.DefaultRequestHeaders.Add("Authorization", jwtToken);

				HttpResponseMessage response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false);

				if (!(response == null || !response.IsSuccessStatusCode))
				{
					result = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false); 
				}
			}

			if (result == string.Empty || result.Contains("Nombre De Cartera Incorrecto"))
			{
				throw null!;
			}

			JObject data = JObject.Parse(result);
			var json = data
					.SelectToken("data")!
					.Value<JToken>();

			JToken cjson = null!;

			if (!json!.First!.SelectToken("cjson")!.ToString().IsNullOrEmpty())
			{
				cjson = json!.First!.SelectToken("cjson")!.Value<JToken>()!;
				json = cjson;
			}

			return json!;
		}
		catch (Exception)
		{
			Debug.WriteLine("No se pudieron mapear los datos");
			throw;
		}
	}
}
