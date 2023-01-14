using Newtonsoft.Json.Linq;

namespace APIsAndJSON
{
    public class RonVSKanyeAPI
    {
        private HttpClient _client;

        public RonVSKanyeAPI(HttpClient client) 
        {
            _client = client;
        }
        const string kanyeURL = "https://api.kanye.rest";
        const string ronURL = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";
        public string Kanye()
        {
            var kanyeResponse = _client.GetStringAsync(kanyeURL).Result;
            var kanyeQuote = JObject.Parse(kanyeResponse).GetValue("quote") != null ? JObject.Parse(kanyeResponse).GetValue("quote").ToString() : string.Empty;
            return kanyeQuote;
        }
        public string RonSwanson()
        {
            var ronResponse = _client.GetStringAsync(ronURL).Result;
            var ronQuote = JArray.Parse(ronResponse);

            return ronQuote[0].ToString();
        }

    }
}
