using System;
using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Turtle.Env;

namespace Turtle.Generators {
	public class GeneratorFactory {
		private HttpClient fetchClient;

		public GeneratorFactory (HttpClient client)
		{
			this.fetchClient = client;
		}

		public async Task<string> GetGeneratorSource (string urlOrUuid)
		{
			string url = urlOrUuid;

			// if we fail to create a uri, fetch the repo and then grab the relevant url
			if (!Uri.TryCreate (urlOrUuid, UriKind.Absolute, out _)) {
				var repo = await this.fetchRepository ();

				url = repo [url];
			}

			// make a request to either the repo url, or direct url
			var sourceResp = await fetchClient.GetAsync (url);
			return await sourceResp.Content.ReadAsStringAsync ();
		}

		private async Task<Dictionary<string, string>> fetchRepository ()
		{
			// fetch the repo
			var response = await fetchClient.GetAsync (VARS.REPO);

			// blegh
			if (!response.IsSuccessStatusCode) {
				return new Dictionary<string, string> ();
			}

			// convert everything to a nice neat object oriented form
			var content = await response.Content.ReadAsStringAsync ();

			if (content == null) {
				return new Dictionary<string, string> ();
			}

			return JsonSerializer.Deserialize<Dictionary<string, string>> (content);
		}

		public async Task<IGenerator> CompileGeneratorSource (string source)
		{
			var scriptResult = await CSharpScript
						.Create (source,
							ScriptOptions.Default
								.WithReferences (typeof (IGenerator).Assembly)
								.WithImports("System", "Turtle.Generators"))
						.RunAsync();
			return (IGenerator)scriptResult.ReturnValue;
		}
	}
}

