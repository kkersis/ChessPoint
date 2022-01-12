using Core;
using Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ApiCallers
{
    public class ChesscomRepository : IChesscomRepository
    {
        private readonly ApiSettings _api;
        private readonly ILogger _logger;

        public ChesscomRepository(ApiSettings api, ILogger logger)
        {
            _api = api;
            _logger = logger;
        }

        public async Task<IEnumerable<ChesscomGame>> GetAllChesscomAsync(string username)
        {
            try
            {
                using HttpClient client = new HttpClient();
                using HttpResponseMessage res = await client.GetAsync(_api.ChesscomApi + "player/" + username + "/games/archives");
                using HttpContent content = res.Content;
                var data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    JObject obj = JObject.Parse(data);
                    JArray arr = (JArray)obj["archives"];
                    List<string> archives = arr.ToObject<List<string>>();
                    IEnumerable<ChesscomGame> games = new List<ChesscomGame>();

                    // chess.com provides archive of monthly games, thus we have to iterate through each month and fetch the games
                    foreach (string endpoint in archives)
                    {
                        games = games.Concat(await GetChesscomGamesByUrl(endpoint, username));
                    }

                    return games;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
            }
            return null;
        }

        // gets games by endpoint - returns ienumerable of games from given endpoint
        private async Task<IEnumerable<ChesscomGame>> GetChesscomGamesByUrl(string endpoint, string username)
        {
            try
            {
                using HttpClient client = new HttpClient();
                using HttpResponseMessage res = await client.GetAsync(endpoint);
                using HttpContent content = res.Content;
                var data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    JObject obj = JObject.Parse(data);
                    JArray arr = (JArray)obj["games"];
                    var games = arr.ToObject<IEnumerable<ChesscomGame>>();
                    return games;
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
