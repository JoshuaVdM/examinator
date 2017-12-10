using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Backend
{
    public class Linguistics
    {
        public HttpClient Client { get; set; }

        public string uri { get; set; }

        public Dictionary<String, String> POSTagset { get; set; }

        public Linguistics()
        {
            Client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            Client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4d53c8a4c5c64e3498dfef62fa2af223");

            uri = "https://westus.api.cognitive.microsoft.com/linguistics/v1.0/analyze";
        }

        public async Task<List<string>> analyze(string text)
        {
            string body = ConstructBody(text);

            HttpResponseMessage response;
            byte[] byteData = Encoding.UTF8.GetBytes(body);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await Client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    Console.Write($"{response.StatusCode}: {response.Content}");
                }

                JArray result = JArray.Parse(await response.Content.ReadAsStringAsync());
                IList<JToken> results = result[0]["result"].Children().ToList();

                List<string> tree = new List<string>();

                foreach (JToken jt in results)
                {
                    tree.Add(jt.ToObject<string>());
                }

                return tree;
            }
        }

        private string ConstructBody(string text)
        {
            JObject jObj = JObject.FromObject(new
            {
                language = "en",
                analyzerIds = new[] { "22a6b758-420f-4745-8a3c-46835a67c0d2" },
                text = text
            });

            return jObj.ToString();
        }

        public async Task<dynamic> GetTree(string text)
        {
            dynamic response;
            response = await analyze(text);
            response = JArray.Parse(response);
            return response[1].result;
        }

        private void InitTags()
        {
            POSTagset.Add("CC", "Coordinating conjunction");
            POSTagset.Add("CD", "Cardinal number");
            POSTagset.Add("DT", "Determiner");
            POSTagset.Add("EX", "Existential there");
            POSTagset.Add("FW", "Foreign word");
            POSTagset.Add("IN", "Preposition or subordinating conjunction");
            POSTagset.Add("JJ", "Adjective");
            POSTagset.Add("JJR", "Adjective, comparative");
            POSTagset.Add("JJS", "Adjective, superlative");
            POSTagset.Add("LS", "List item marker");
            POSTagset.Add("MD", "Modal");
        }
    }
}
