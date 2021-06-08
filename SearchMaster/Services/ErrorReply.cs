using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
namespace SearchMaster
{
    public class Errors
    {
        public string CityNotFound
        {
            get; set;
        }
        public async Task<string> ErrorReplyCityNotFound()
        {

            var e = JsonSerializer.Deserialize<Errors>(File.ReadAllText("../../Errors.json")).CityNotFound;
            return e;

        }

    }
}