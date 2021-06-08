using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
namespace SearchMaster
{
    public class Vers
    {
        public string book_id { get; set; }
        public string book_name { get; set; }
        public int chapter { get; set; }
        public int verse { get; set; }
        public string text { get; set; }
    }

    public class BibleApi
    {
        public string reference { get; set; }
        public List<Vers> verses { get; set; }
        public string text { get; set; }
        public string translation_id { get; set; }
        public string translation_name { get; set; }
        public string translation_note { get; set; }
    }
    public class BibleApiSearch
    {
        public async Task<BibleApi> BibleApiAsync(string book, string chapterAndVerse)
        {
            HttpClient client = new HttpClient();
            var getUrl = await client.GetAsync("https://bible-api.com/" + book + " " + chapterAndVerse);
            var deserialize = JsonSerializer.Deserialize<BibleApi>(await getUrl.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return deserialize;
        }
    }
}