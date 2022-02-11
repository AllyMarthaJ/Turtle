using System;
using Turtle.Env;

namespace Turtle.Generators
{
	public class GeneratorFactory
	{
		private HttpClient fetchClient;

		public GeneratorFactory (HttpClient client)
		{
			this.fetchClient = client;
		}

		public async Task<string> GetGeneratorSource(string urlOrUuid)
		{
			string url = urlOrUuid;

			if (Guid.TryParse(urlOrUuid, out Guid guid)) {
				var repo = await this.fetchRepository ();

				url = repo [guid];
			}

			var sourceResp = await fetchClient.GetAsync (url);
			return await sourceResp.Content.ReadAsStringAsync ();
		}

		private async Task<Dictionary<Guid, string>> fetchRepository()
		{
			var response = await fetchClient.GetAsync (VARS.REPO);

			if (!response.IsSuccessStatusCode) {
				return new Dictionary<Guid, string> ();
			}

			var content = await response.Content.ReadAsStringAsync();

			var repository = new Dictionary<Guid, string> ();

			foreach (var line in content.Split(Environment.NewLine)) {
				var entry = line.Split (" : ");
				repository.Add (Guid.Parse (entry[0]), entry [1]);
			}

			return repository;
		}
	}
}

