using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FightCore.KuroganeHammer.Models;
using Newtonsoft.Json;

namespace FightCore.KuroganeHammer.Services
{
    public interface IKuroganeHammerService
    {
        Task<List<CharacterAttributes>> GetCharacterAttributes(string name, string game = "smash4");
    }

    public class KuroganeHammerService : IKuroganeHammerService
    {
        private const string BaseUrl = "https://api.kuroganehammer.com/api/";

        public async Task<List<CharacterAttributes>> GetCharacterAttributes(string name, string game = "smash4")
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            // Add an Accept header for JSON format.  
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.  
            var response = await client.GetAsync($"characters/name/{name}/characterattributes?game={game}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CharacterAttributes>>(
                    await response.Content.ReadAsStringAsync());
            }

            return null;

        }
    }
}
