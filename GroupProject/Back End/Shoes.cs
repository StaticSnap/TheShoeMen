using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
using System.Text.Json;

namespace GroupProject
{
    public static class Shoes
    {
        private static List<Shoe> shoes = new List<Shoe>();

        public async static Task Init(HttpClient Http)
        {
            if (shoes.Count != 0)
            {
                return;
            }

            var query = new
            {
                type = "select",
                parameters = new
                {
                    table = "shoe"
                }
            };

            var response = await Http.PostAsJsonAsync("http://localhost:8080/api/query", query);
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw JSON response: {jsonString}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            shoes = JsonSerializer.Deserialize<List<Shoe>>(jsonString, options);
            
            // Debug: Print loaded shoes
            foreach (var shoe in shoes)
            {
                Console.WriteLine($"Loaded Shoe - ID: {shoe.shoeID}, Model: {shoe.model}, ImagePath: {shoe.imagePath ?? "null"}");
            }
        }

        public static List<Shoe> GetShoes()
        {
            return shoes;
        }

        public static Shoe GetShoeFromID(int id)
        {
            return shoes.FirstOrDefault(s => s.shoeID == id) ?? throw new Exception("Shoe not found");
        }
    }
}
