using Core;
using Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ApiCallers
{
    public class LichessRepository : ILichessRepository
    {
        private readonly ApiSettings _api;
        private readonly ILogger<LichessRepository> _logger;

        public LichessRepository(ApiSettings api, ILogger<LichessRepository> logger)
        {
            _api = api;
            _logger = logger;
        }

        public async Task<IEnumerable<LichessGame>> GetAllLichessAsync(string username)
        {
            try
            {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-ndjson"));
                using HttpResponseMessage response = await client.GetAsync(_api.LichessApi + "games/user/" + username + "?opening=true");
                using HttpContent content = response.Content;

                var data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    using JsonTextReader jsonReader = new JsonTextReader(new StringReader(data))
                    {
                        SupportMultipleContent = true
                    };

                    var lichessGames = new List<LichessGame>();

                    var jsonSerializer = new JsonSerializer();
                    while (jsonReader.Read())
                    {
                        LichessGame liGame = jsonSerializer.Deserialize<LichessGame>(jsonReader);
                        lichessGames.Add(liGame);
                    }

                    return lichessGames;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
            }
            return null;
        }
    }
}
