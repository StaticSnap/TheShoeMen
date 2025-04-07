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
            // If the shoes have been loaded before. then don't bother loading them again.
            if (shoes.Count != 0)
            {
                return;
            }
            var query = $"select * from shoe";

            var response = await Http.PostAsync("http://localhost:8080", new StringContent(query));

            shoes = await response.Content.ReadFromJsonAsync<List<Shoe>>();

            foreach(Shoe shoe in shoes)
            {
                await shoe.Init(Http);
            }
        }

        public static List<Shoe> GetShoes()
        {
            return shoes;
        }

        public static Shoe GetShoeFromID(int id)
        {
            foreach (var shoe in shoes)
            {
                if (shoe.shoeID == id)
                {
                    return shoe;
                }
            }
            throw new Exception("Shoe not found");
        }
    }
}
